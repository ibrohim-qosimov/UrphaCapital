using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Courses.Queries;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Courses.Handlers.QueryHandlers
{
    public class GetAllCoursesByMentorIdQueryHandler : IRequestHandler<GetAllCoursesByMentorIdQuery, IEnumerable<Course>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public GetAllCoursesByMentorIdQueryHandler(IApplicationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Course>> Handle(GetAllCoursesByMentorIdQuery request, CancellationToken cancellationToken)
        {

            var value = _memoryCache.Get("course");

            if (value == null)
            {
                _memoryCache.Set(
                        key: "course",
                        value: await _context.Courses
                            .Where(l => l.MentorId == request.MentorId)
                                .Skip(request.Index - 1)
                                    .Take(request.Count)
                                        .Select(x => new Course()
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            MentorId = x.MentorId
                                        })
                                        .ToListAsync(cancellationToken),

                         options: new MemoryCacheEntryOptions()
                         {
                             SlidingExpiration = TimeSpan.FromSeconds(5),
                             AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20),
                             Size = 2048
                         });

            }

            return _memoryCache.Get("course") as IEnumerable<Course>;
        }
    }
}
