using MediatR;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.AnnouncementCRUD.Queries
{
    public class GetAllAnnouncementsQuery : IRequest<IEnumerable<Announcement>>
    {
    }
}