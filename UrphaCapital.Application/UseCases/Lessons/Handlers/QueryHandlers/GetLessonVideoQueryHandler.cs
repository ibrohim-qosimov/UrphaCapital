using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Lessons.Queries;

namespace UrphaCapital.Application.UseCases.Lessons.Handlers.QueryHandlers
{
    internal class GetLessonVideoQueryHandler : IRequestHandler<GetLessonVideoQuery, Stream?>
    {
        private readonly IApplicationDbContext _context;

        public GetLessonVideoQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stream?> Handle(GetLessonVideoQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id.ToString() == request.Id);
            if (lesson == null)
            {
                return null;
            }
            var videoPath = "wwwroot" + lesson.Video;

            if (!System.IO.File.Exists(videoPath))
            {
                return null;
            }
            return await Task.FromResult(System.IO.File.OpenRead(videoPath));
        }
    }
}
