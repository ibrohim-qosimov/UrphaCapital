using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Mentors.Queries;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Mentors.Handlers.QueryHandlers
{
    public class GetAllMentorsQueryHandler : IRequestHandler<GetAllMentorsQuery, IEnumerable<Mentor>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllMentorsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mentor>> Handle(GetAllMentorsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Mentors.ToListAsync(cancellationToken);
        }
    }
}
