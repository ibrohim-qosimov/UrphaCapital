using MediatR;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.AnnouncementCRUD.Commands
{
    public class CreateAnnouncementCommand : IRequest<ResponseModel>
    {
        public string Title { get; set; }
    }
}
