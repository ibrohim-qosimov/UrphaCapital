using MediatR;
using Microsoft.AspNetCore.Http;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.IdeasCrud.Commands
{
    public class UpdateIdeaCommand : IRequest<ResponseModel>
    {
        public long Id { get; set; }
        public IFormFile? Picture { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
