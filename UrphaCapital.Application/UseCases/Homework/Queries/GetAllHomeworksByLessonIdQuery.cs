using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Homework.Queries
{
    public class GetAllHomeworksByLessonIdQuery : IRequest<IEnumerable<Homeworks>>
    {
        public long LessonId { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
