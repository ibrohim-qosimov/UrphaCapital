using MediatR;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.AnnouncementCRUD.Commands
{
    public class DeleteAnnouncementCommand : IRequest<ResponseModel>
    {
        public long Id { get; set; }
    }
}
