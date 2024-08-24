using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class GetAllHomeworksByLessonIdQueryHandler : IRequestHandler<GetAllHomeworksByLessonIdQuery, IEnumerable<Homeworks>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllHomeworksByLessonIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Homeworks>> Handle(GetAllHomeworksByLessonIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Homeworks
                .Where(x => x.LessonId == request.LessonId)
                .ToListAsync(cancellationToken);
        }
    }
}
