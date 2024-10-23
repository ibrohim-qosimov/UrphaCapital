using MediatR;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Results.Queries;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Results.Handlers.QueryHandlers
{
    public class GetResultsQueryHandler : IRequestHandler<GetResultsQuery, IEnumerable<Result>>
    {
        private readonly IApplicationDbContext _context;

        public GetResultsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Result>> Handle(GetResultsQuery request, CancellationToken cancellationToken)
        {
            return _context.Results.Take(request.Size);
        }
    }
}
