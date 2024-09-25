using MediatR;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Help_.Queries
{
    public class GetAllHelpsQuery : IRequest<IEnumerable<Help>>
    {
    }
}
