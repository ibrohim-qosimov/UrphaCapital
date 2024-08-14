using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Courses.Queries;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Courses.Handlers.QueryHandlers
{
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, IEnumerable<Course>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public GetAllCoursesQueryHandler(IApplicationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Course>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var value = _memoryCache.Get("course-all");

            if (value == null)
            {
                _memoryCache.Set(
                        key: "course-all",
                        value: await _context.Courses.ToListAsync(cancellationToken),
                         options: new MemoryCacheEntryOptions()
                         {
                             SlidingExpiration = TimeSpan.FromSeconds(5),
                             AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20),
                             Size = 2048
                         });

            }

            return _memoryCache.Get("course-all") as IEnumerable<Course>;
        }
    }
}
