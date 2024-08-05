using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Models;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Commands
{
    public class DeleteStudentCommand: IRequest<ResponseModel>
    {
        public long Id { get; set; }
    }
}
