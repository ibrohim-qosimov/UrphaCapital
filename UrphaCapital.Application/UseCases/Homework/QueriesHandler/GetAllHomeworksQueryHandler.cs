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
    public class GetAllHomeworksQueryHandler : IRequestHandler<GetAllHomeworksQuery, IEnumerable<Homeworks>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllHomeworksQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Homeworks>> Handle(GetAllHomeworksQuery request, CancellationToken cancellationToken)
        {
            return await _context.Homeworks
                .Skip(request.Index - 1)
                .Take(request.Count)
                .Include(x => x.Lesson)
                .ToListAsync(cancellationToken);
        }
    }
}
