using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.IdeasCrud.Commands
{
    public class CreateIdeaCommand: IRequest<ResponseModel>
    {
        public IFormFile Picture { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
