using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Lessons.Queries
{
    public class GetAllLessonsByCourseIdQuery : IRequest<IEnumerable<Lesson>>
    {
        public string CourseId { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
