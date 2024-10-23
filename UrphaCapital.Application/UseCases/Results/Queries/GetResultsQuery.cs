using MediatR;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Results.Queries
{
    public class GetResultsQuery : IRequest<IEnumerable<Result>>
    {
        public short Size { get; set; }
    }
}
