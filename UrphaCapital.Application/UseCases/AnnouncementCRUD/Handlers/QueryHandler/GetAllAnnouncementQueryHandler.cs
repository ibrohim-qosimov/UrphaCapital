using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.AnnouncementCRUD.Queries;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.AnnouncementCRUD.Handlers.QueryHandler
{
    public class GetAllAnnouncementsQueryHandler : IRequestHandler<GetAllAnnouncementsQuery, IEnumerable<Announcement>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllAnnouncementsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Announcement>> Handle(GetAllAnnouncementsQuery request, CancellationToken cancellationToken)
        {
            var announcements = await _context.Announcements.ToListAsync(cancellationToken);
            return announcements;
        }
    }
}
