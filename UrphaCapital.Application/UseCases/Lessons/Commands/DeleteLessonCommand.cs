using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Lessons.Commands
{
    public class DeleteLessonCommand : IRequest<ResponseModel>
    {
        public long Id { get; set; }
    }
}
