using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Homework.Queries;
using UrphaCapital.Domain.DTOs;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Homework.QueriesHandler
{
    public class GetStudentHomeworkResultsQueryHandler : IRequestHandler<GetStudentHomeworkResultsQuery, IEnumerable<HomeworkResultView>>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentHomeworkResultsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HomeworkResultView>> Handle(GetStudentHomeworkResultsQuery request, CancellationToken cancellationToken)
        {
            var homeworkList = await _context.Homeworks
                          .Where(h => h.StudentId == request.StudentId)
                          .Skip(request.Index - 1)
                          .Take(request.Count)
                          .Include(h => h.Lesson)
                          .ToListAsync();

            var result = homeworkList.Select(h => new HomeworkResultView()
            {
                HomeworkId = h.Id,
                FILE = h.FILE,
                Grade = h.Grade,
                LessonTitle = h.Lesson.Title
            });

            return result;

        }
    }
}
