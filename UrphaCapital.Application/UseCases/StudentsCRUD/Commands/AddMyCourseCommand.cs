using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Commands
{
    public class AddMyCourseCommand : IRequest<ResponseModel>
    {
        public long StudentId { get; set; }
        public Guid CourseId { get; set; }
    }
}
