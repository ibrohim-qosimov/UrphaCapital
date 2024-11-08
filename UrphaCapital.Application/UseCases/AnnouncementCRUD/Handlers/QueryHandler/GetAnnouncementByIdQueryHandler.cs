using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.AnnouncementCRUD.Queries;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.AnnouncementCRUD.Handlers.QueryHandler
{
    public class GetAnnouncementByIdQueryHandler : IRequestHandler<GetAnnouncementByIdQuery, Announcement>
    {
        private readonly IApplicationDbContext _context;

        public GetAnnouncementByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Announcement> Handle(GetAnnouncementByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Announcements.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
