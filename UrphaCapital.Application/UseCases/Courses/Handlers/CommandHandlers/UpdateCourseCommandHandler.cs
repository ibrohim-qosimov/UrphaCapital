using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.UseCases.Courses.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Courses.Handlers.CommandHandlers
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, ResponseModel>
    {
    }
}
