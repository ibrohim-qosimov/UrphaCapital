using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Courses.Commands
{
    public class UpdateCourseCommand : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Subtitle { get; set; }
        public IFormFile? Picture { get; set; }
        public string? Price { get; set; }
        public long? MentorId { get; set; }
    }
}
