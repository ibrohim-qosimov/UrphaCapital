using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Homework.Commands
{
    public class CreateHomeworkCommand: IRequest<ResponseModel>
    {
        public string Title { get; set; }
        public IFormFile FILE { get; set; }
        public string Description { get; set; }
        public long studentId { get; set; }
        public long LessonId { get; set; }
    }
}
