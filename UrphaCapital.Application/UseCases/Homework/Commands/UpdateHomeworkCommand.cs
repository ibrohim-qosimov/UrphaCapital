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
    public class UpdateHomeworkCommand: IRequest<ResponseModel>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public IFormFile FILE { get; set; }
        public string Description { get; set; }
        public long studentId { get; set; }
        public long LessonId { get; set; }
    }
}
