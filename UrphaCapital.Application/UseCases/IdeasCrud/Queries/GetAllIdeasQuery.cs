using MediatR;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.IdeasCrud.Queries
{
    public class GetAllIdeasQuery : IRequest<IEnumerable<Ideas>>
    {
    }
}
