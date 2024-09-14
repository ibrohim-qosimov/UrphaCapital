using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Homework.Commands
{
    public class GradeHomeworkCommand : IRequest<ResponseModel>
    {
        public long HomeworkId { get; set; }
        public long MentorId { get; set; }
        public int Grade { get; set; }
    }
}
