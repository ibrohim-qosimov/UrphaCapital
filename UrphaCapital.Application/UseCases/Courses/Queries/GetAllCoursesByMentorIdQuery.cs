using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Courses.Queries
{
    public class GetAllCoursesByMentorIdQuery : IRequest<IEnumerable<Course>>
    {
        public long MentorId { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
