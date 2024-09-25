using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Mentors.Queries;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Mentors.Handlers.QueryHandlers
{
    public class GetMentorByIdQueryHandler : IRequestHandler<GetMentorByIdQuery, Mentor>
    {
        private readonly IApplicationDbContext _context;

        public GetMentorByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Mentor> Handle(GetMentorByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Mentors.FirstOrDefaultAsync(x => x.Id == request.Id) ??
                 throw new Exception("Not found");
        }
    }
}
