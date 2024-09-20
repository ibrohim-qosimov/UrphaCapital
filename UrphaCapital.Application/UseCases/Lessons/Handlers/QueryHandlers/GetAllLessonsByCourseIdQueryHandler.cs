using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Lessons.Queries;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Lessons.Handlers.QueryHandlers
{
    public class GetAllLessonsByCourseIdQueryHandler : IRequestHandler<GetAllLessonsByCourseIdQuery, IEnumerable<Lesson>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllLessonsByCourseIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lesson>> Handle(GetAllLessonsByCourseIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Lessons
                .Where(l => l.CourseId.ToString() == request.CourseId)
                .Skip(request.Index - 1)
                .Take(request.Count)
                .Select(x => new Lesson
                {
                    Id = x.Id,
                    Title = x.Title,
                    Video = x.Video,
                    HomeworkDescription = x.HomeworkDescription,
                    CourseId = x.CourseId
                })
                .ToListAsync(cancellationToken);
        }
    }
}
