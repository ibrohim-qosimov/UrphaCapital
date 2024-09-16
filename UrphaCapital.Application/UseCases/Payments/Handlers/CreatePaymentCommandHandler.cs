using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Payments.Commands;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Payments.Handlers
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;

        public CreatePaymentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment()
            {
                StudentId = request.StudentId,
                CourseId = request.CourseId,
                Amount = request.Amount,
                PaymentStatus = request.PaymentStatus,
            };

            await _context.Paymentss.AddAsync(payment);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                Message = "Created",
                StatusCode = 200,
                IsSuccess = true
            };
        }
    }
}
