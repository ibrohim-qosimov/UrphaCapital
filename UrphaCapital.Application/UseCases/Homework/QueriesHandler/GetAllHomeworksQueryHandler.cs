using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Homework.Queries;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Homework.QueriesHandler
{
    public class GetAllHomeworksQueryHandler : IRequestHandler<GetAllHomeworksQuery,IEnumerable<Homeworks>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMemoryCache _memoryCache;
        public GetAllHomeworksQueryHandler(IApplicationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Homeworks>> Handle(GetAllHomeworksQuery request, CancellationToken cancellationToken)
        {
            var value = _memoryCache.Get("homework");

            if (value == null)
            {
                _memoryCache.Set(
                        key: "homework",
                        value: await _context.Homeworks
                .ToListAsync(cancellationToken),
                         options: new MemoryCacheEntryOptions()
                         {
                             SlidingExpiration = TimeSpan.FromSeconds(5),
                             AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20),
                             Size = 2048
                         });

            }
            return _memoryCache.Get("homework") as IEnumerable<Homeworks>;
        }
    }
}
