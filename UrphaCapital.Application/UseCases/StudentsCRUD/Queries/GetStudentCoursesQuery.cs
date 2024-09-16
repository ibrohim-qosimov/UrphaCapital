using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Queries
{
    public class GetStudentCoursesQuery : IRequest<IEnumerable<Course>>
    {
        public long Id { get; set; }
    }
}
