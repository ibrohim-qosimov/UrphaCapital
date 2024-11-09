using MediatR;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.AnnouncementCRUD.Queries
{
    public class GetAnnouncementByIdQuery : IRequest<Announcement>
    {
        public long Id { get; set; }
    }
}
