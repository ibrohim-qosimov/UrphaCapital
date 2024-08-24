using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class GetAllHomeworksByMentorIdQueryHandler : IRequestHandler<GetAllHomeworksByMentorIdQuery, IEnumerable<Homeworks>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllHomeworksByMentorIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Homeworks>> Handle(GetAllHomeworksByMentorIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Homeworks
                .Include(x => x.Lesson)
                    .ThenInclude(x => x.Course)
                        .Where(x => x.Lesson.Course.MentorId == request.MentorId)
                            .ToListAsync(cancellationToken);
        }
    }
}
