using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Queries
{
    public class GetAllStudentsQuery: IRequest<IEnumerable<Student>>
    {
        public long Id { get; set; }
    }
}
