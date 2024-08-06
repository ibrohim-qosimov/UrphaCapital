using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Mentors.Commands
{
    public class DeleteMentorCommand : IRequest<ResponseModel>
    {
        public long Id { get; set; }
    }
}
