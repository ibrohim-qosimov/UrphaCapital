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
        private readonly IMemoryCache _memoryCache;

        public GetAllLessonsByCourseIdQueryHandler(IApplicationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Lesson>> Handle(GetAllLessonsByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var value = _memoryCache.Get("lesson");

            if (value == null)
            {
                _memoryCache.Set(
                        key: "lesson",
                        value: await _context.Lessons
            .Where(l => l.CourseId == request.CourseId)
                .Skip(request.Index - 1)
                    .Take(request.Count)
                .Select(x => new Lesson
                {
                    Id = x.Id,
                    Name = x.Name,
                    CourseId = x.CourseId
                })
                .ToListAsync(cancellationToken),
                         options: new MemoryCacheEntryOptions()
                         {
                             SlidingExpiration = TimeSpan.FromSeconds(5),
                             AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20),
                             Size = 2048
                         });

            }

            return _memoryCache.Get("lesson") as IEnumerable<Lesson>;

        }
    }
}
