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
    public class GetLessonByIdQueryHandler : IRequestHandler<GetLessonByIdQuery, Lesson>
    {
        private readonly IApplicationDbContext _context;

        public GetLessonByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Lesson> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Lessons.FirstOrDefaultAsync(x => x.Id == request.Id) ??
                    throw new NotImplementedException();
        }
    }
}
