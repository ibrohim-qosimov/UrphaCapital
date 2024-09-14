using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Lessons.Commands
{
    public class UpdateLessonCommand : IRequest<ResponseModel>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Guid CourseId { get; set; }
        public IFormFile Video { get; set; }
        public string HomeworkDescription { get; set; }
    }
}
