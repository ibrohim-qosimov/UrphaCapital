using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Mentors.Queries;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Mentors.Handlers.QueryHandlers
{
    public class GetMentorByEmailQueryHandler : IRequestHandler<GetAMentorByEmailQuery, Mentor?>
    {
        private readonly IApplicationDbContext _context;

        public GetMentorByEmailQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Mentor?> Handle(GetAMentorByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _context.Mentors
                .FirstOrDefaultAsync(x => x.Email == request.Email);
        }
    }
}
