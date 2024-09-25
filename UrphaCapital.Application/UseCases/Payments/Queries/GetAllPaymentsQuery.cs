using MediatR;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Payments.Queries
{
    public class GetAllPaymentsQuery : IRequest<IEnumerable<Payment>>
    {
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
