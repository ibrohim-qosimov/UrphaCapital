using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Courses.Commands
{
    public class DeleteCourseCommand : IRequest<ResponseModel>
    {
        public string Id { get; set; }
    }
}
