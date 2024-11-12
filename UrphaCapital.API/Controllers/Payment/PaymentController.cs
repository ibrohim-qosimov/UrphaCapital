using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using UrphaCapital.API.Configurations;
using UrphaCapital.Application.ExternalServices.PaymentProcessing;
using UrphaCapital.Application.UseCases.ClickTransactions.Commands.CreateCommand;
using UrphaCapital.Application.UseCases.ClickTransactions.Commands.UpdateCommand;
using UrphaCapital.Application.UseCases.Courses.Queries;
using UrphaCapital.Application.UseCases.StudentsCRUD.Queries;
using UrphaCapital.Application.ViewModels.PaymentModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.API.Controllers.Payment
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMediator _mediator;
        private readonly ClickConfig _clickConfig;
        public PaymentController(IPaymentService paymentService, IConfiguration configuration, IMediator mediator)
        {
            _clickConfig = configuration.GetSection("ClickConfig").Get<ClickConfig>()!;
            _paymentService = paymentService;
            _mediator = mediator;
        }

        [HttpPost("prepare")]
        public async Task<IActionResult> Prepare()
        {
            var form = Request.Form;
            var clickTransId = long.Parse(form["click_trans_id"]);
            var serviceId = int.Parse(form["service_id"]);
            var clickPaydocId = form["click_paydoc_id"];
            var merchantTransId = form["merchant_trans_id"];
            var amount = decimal.Parse(form["amount"]);
            var action = int.Parse(form["action"]);
            var signTime = form["sign_time"];
            var error = form["error"];
            var errorNote = form["error_note"];
            var signString = form["sign_string"];

            var generatedSignString = GenerateSignString(
                                clickTransId,
                                serviceId,
                                _clickConfig.SecretKey,
                                merchantTransId,
                                amount,
                                action,
                                signTime);

            if (signString != generatedSignString)
                return Ok(new { error = -1, error_note = "Sign check failed!" });


            #region order and payer exists check

            var courseTypeCheck = int.TryParse(merchantTransId.ToString().Split(":")[0], out int courseId);
            var studentTypeCheck = long.TryParse(merchantTransId.ToString().Split(":")[1], out long studentId);

            if (courseTypeCheck != true && studentTypeCheck != true)
                return BadRequest(new { error = -5, error_note = "Invoice not found!" });

            var courseQuery = new GetCourseByIdQuery()
            {
                Id = courseId
            };

            var course = await _mediator.Send(courseQuery);

            if (course == null)
                return BadRequest(new { error = -5, error_note = "Invoice not found!" });

            if (course.Price != amount)
                return BadRequest(new { error = -5, error_note = "Amount is less!" });

            var studentQuery = new GetAllStudentsByIdQuery()
            {
                Id = studentId
            };

            var student = await _mediator.Send(studentQuery);

            if (student == null)
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

            var clickTransactionCommand = new CreateTransactionCommand()
            {
                ClickTransId = clickTransId,
                MerchantTransId = merchantTransId,
                Amount = amount,
                SignTime = signTime,
                Situation = action
            };

            var clickTransResponse = await _mediator.Send(clickTransactionCommand);

            if (clickTransResponse == null)
            {
                Console.WriteLine("!! Diqqat !! transaksiy saqlanmadi! {0} : {1}", courseId, studentId);
            }

            #endregion


            var response = new PrepareResponse()
            {
                ClickTransId = clickTransId,
                MerchantTransId = merchantTransId,
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


            if (action == 0)
                return Ok(new { error = -3, error_note = "Action not found!" });

            if (action == -4)
                return Ok(new { error = -4, error_note = "Already paid!" });

            if (action == -9)
            {
                var errorTransaction = await _mediator.Send(new UpdateTransactionCommand()
                {
                    TransactionId = clickTransId,
                    Status = EOrderPaymentStatus.Failed,
                });

                if (errorTransaction.IsSuccess == false && errorTransaction.StatusCode == 404)
                    return Ok(new { error = -6, error_note = "Transaction isn't exist" });

                return Ok(new { error = -9, error_note = "Transaction cancelled!" });
            }
            #endregion

            var updateCommand = new UpdateTransactionCommand()
            {
                TransactionId = clickTransId,
                Status = EOrderPaymentStatus.Paid,
            };

            var updateTransaction = await _mediator.Send(updateCommand);

            if (updateTransaction.IsSuccess == false && updateTransaction.StatusCode == 404)
                return Ok(new { error = -6, error_note = "Transaction isn't exist" });

            return Ok(new ComplateResponse()
            {
                ClickTransId = clickTransId,
                MerchantTransId = merchantTransId,
                MerchantConfirmId = action,
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
