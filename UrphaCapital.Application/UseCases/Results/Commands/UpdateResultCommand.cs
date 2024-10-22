using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Results.Commands
{
    public class UpdateResultCommand : IRequest<ResponseModel>
    {
        public long Id { get; set; }
        public IFormFile? Picture { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
