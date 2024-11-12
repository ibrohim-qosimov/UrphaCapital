using MediatR;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.AnnouncementCRUD.Commands
{
    public class UpdateAnnouncementCommand : IRequest<ResponseModel>
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
}