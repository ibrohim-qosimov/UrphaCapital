using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Lessons.Commands;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Lessons.Handlers.CommandHandlers
{
    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMemoryCache _memoryCache;

        public CreateLessonCommandHandler(IApplicationDbContext context, IWebHostEnvironment web, IMemoryCache memoryCache)
        {
            _context = context;
            _webHostEnvironment = web;
            _memoryCache = memoryCache;
        }
        public async Task<ResponseModel> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var file = request.Video;
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "LessonVideos");
            string fileName = "";

            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    Debug.WriteLine("Directory created successfully.");
                }

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(_webHostEnvironment.WebRootPath, "LessonVideos", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Message = ex.Message,
                    StatusCode = 500,
                    IsSuccess = false
                };
            }

            var category = new Lesson()
            {
                Title = request.Name,
                CourseId = request.CourseId,
                HomeworkDescription = request.HomeworkDescription,
                Video = "/LessonVideos/" + fileName,
            };

            await _context.Lessons.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _memoryCache.Remove("lesson");

            return new ResponseModel()
            {
                IsSuccess = true,
                Message = "Created",
                StatusCode = 201,
            };
        }
    }
}
