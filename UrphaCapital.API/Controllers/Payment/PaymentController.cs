using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UrphaCapital.API.Configurations;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.ExternalServices.PaymentProcessing;
using UrphaCapital.Application.UseCases.Courses.Queries;
using UrphaCapital.Application.ViewModels.PaymentModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.API.Controllers.Payment
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IApplicationDbContext _context;
        private readonly IPaymentService _paymentService;
        private readonly IMediator _mediator;
        private readonly ClickConfig _clickConfig;
        public PaymentController(IPaymentService paymentService, IConfiguration configuration, IApplicationDbContext context, IMediator mediator)
        {
            _clickConfig = configuration.GetSection("ClickConfig").Get<ClickConfig>()!;
            _paymentService = paymentService;
            _context = context;
            _mediator = mediator;
        }

        [HttpPost("prepare")]
        public async Task<IActionResult> Prepare()
        {
            var form = Request.Form;
            var clickTransId = form["click_trans_id"];
            var serviceId = form["service_id"];
            var clickPaydocId = form["click_paydoc_id"];
            var merchantTransId = form["merchant_trans_id"];
            var amount = form["amount"];
            var action = form["action"];
            var signTime = form["sign_time"];
            var error = form["error"];
            var errorNote = form["error_note"];
            var signString = form["sign_string"];

            var generatedSignString = GenerateSignString(
                                long.Parse(clickTransId),
                                int.Parse(serviceId),
                                _clickConfig.SecretKey,
                                merchantTransId,
                                decimal.Parse(amount),
                                int.Parse(action),
                                signTime);

            if (signString != generatedSignString)
                return Ok(new { error = -1, error_note = "Sign check failed!" });

            #region order exists check

            var query = new GetCourseByIdQuery()
            {
                Id = merchantTransId
            };

            var course = await _mediator.Send(query);

            if (course == null)
                return BadRequest(new { error = -5, error_note = "Invoice not found!" });
            #endregion

            #region validation

            if (action != 0)
                return Ok(new { error = -3, error_note = "Action not found!" });

            if (action == -4)
                return Ok(new { error = -4, error_note = "Already paid!" });

            if (action == -9)
                return Ok(new { error = -9, error_note = "Transaction cancelled!" });

            #endregion

            #region save transaction
            var clickTransaction = new ClickTransaction
            {
                ClickTransId = long.Parse(clickTransId),
                MerchantTransId = merchantTransId,
                Amount = decimal.Parse(amount),
                SignTime = signTime,
                Status = EOrderPaymentStatus.Pending,
            };

            _context.ClickTransactions.Add(clickTransaction);
            await _context.SaveChangesAsync();

            #endregion


            var response = new PrepareResponse()
            {
                ClickTransId = clickTransaction.ClickTransId,
                MerchantTransId = clickTransaction.MerchantTransId,
                MerchantPrepareId = 1,
                Error = 0,
                ErrorNote = "Payment prepared successfully"
            };

            return Ok(response);
        }

        [HttpPost("complete")]
        public async Task<IActionResult> Complete()
        {
            var form = Request.Form;

            var clickTransId = long.Parse(form["click_trans_id"]);
            var serviceId = int.Parse(form["service_id"]);
            var clickPaydocId = long.Parse(form["click_paydoc_id"]);
            var merchantTransId = form["merchant_trans_id"];
            var merchantPrepareId = int.Parse(form["merchant_prepare_id"]);
            var amount = decimal.Parse(form["amount"]);
            var action = int.Parse(form["action"]);
            var error = int.Parse(form["error"]);
            var errorNote = form["error_note"];
            var signTime = form["sign_time"];
            var signString = form["sign_string"];


            var generatedSignString = GenerateSignString(
                clickTransId,
                serviceId,
                _clickConfig.SecretKey,
                merchantTransId,
                merchantPrepareId,
                amount,
                action,
                signTime);

            #region validation 

            if (signString != generatedSignString)
                return Ok(new { error = -5, error_note = "Sign check failed!" });

            var clickTransaction = await _context.ClickTransactions.Where(c => c.ClickTransId == clickTransId && c.Status == EOrderPaymentStatus.Pending).FirstOrDefaultAsync(); ;

            if (clickTransaction != null)
                clickTransaction.Status = EOrderPaymentStatus.Paid;

            if (action != 0)
                return Ok(new { error = -3, error_note = "Action not found!" });

            if (action == -4)
                return Ok(new { error = -4, error_note = "Already paid!" });

            if (action == -9)
            {
                clickTransaction.Status = EOrderPaymentStatus.Failed;
                return Ok(new { error = -9, error_note = "Transaction cancelled!" });
            }

            await _context.SaveChangesAsync();

            #endregion
           
            return Ok(new ComplateResponse()
            {
                ClickTransId = clickTransaction.ClickTransId,
                MerchantTransId = clickTransaction.MerchantTransId,
                MerchantConfirmId = clickTransaction.Id,
                Error = 0,
                ErrorNote = "Success"
            });
        }


        private string GenerateSignString(long clickTransId, int serviceId, string secretKey, string merchantTransId, decimal amount, int action, string signTime)
        {
            var signString = $"{clickTransId}{serviceId}{secretKey}{merchantTransId}{amount}{action}{signTime}";
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(signString);
                var hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }
        private string GenerateSignString(long clickTransId, int serviceId, string secretKey, string merchantTransId, int merchantPrepareId, decimal amount, int action, string signTime)
        {
            var signString = $"{clickTransId}{serviceId}{secretKey}{merchantTransId}{merchantPrepareId}{amount}{action}{signTime}";

            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(signString);
                var hashBytes = md5.ComputeHash(inputBytes);

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }

    }
}
