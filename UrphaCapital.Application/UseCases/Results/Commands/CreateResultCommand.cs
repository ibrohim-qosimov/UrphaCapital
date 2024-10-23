using MediatR;
using Microsoft.AspNetCore.Http;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Results.Commands
{
    public class CreateResultCommand : IRequest<ResponseModel>
    {
        public IFormFile Picture { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
