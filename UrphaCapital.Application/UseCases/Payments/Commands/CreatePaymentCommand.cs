using MediatR;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Payments.Commands
{
    public class CreatePaymentCommand : IRequest<ResponseModel>
    {
        public long StudentId { get; set; }
        public Guid CourseId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
    }
}
