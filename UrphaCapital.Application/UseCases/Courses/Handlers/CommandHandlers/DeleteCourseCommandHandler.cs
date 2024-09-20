using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Courses.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Courses.Handlers.CommandHandlers
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMemoryCache _memoryCache;

        public DeleteCourseCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IMemoryCache memoryCache)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _memoryCache = memoryCache;
        }

        public async Task<ResponseModel> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (course == null)
            {
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Not found",
                    StatusCode = 404,
                };
            }

            var deleteFilePath = Path.Combine("wwwroot", _webHostEnvironment.WebRootPath, course.Picture);

            if (File.Exists(deleteFilePath))
                File.Delete(deleteFilePath);

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync(cancellationToken);


            _memoryCache.Remove("course-all");
            _memoryCache.Remove("course");

            return new ResponseModel()
            {
                IsSuccess = true,
                Message = "Deleted",
                StatusCode = 201,
            };
        }
    }
}
