using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Lessons.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Lessons.Handlers.CommandHandlers
{
    public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UpdateLessonCommandHandler(IApplicationDbContext context, IWebHostEnvironment webhost)
        {
            _webHostEnvironment = webhost;
            _context = context;
        }

        public async Task<ResponseModel> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (lesson == null)
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Lesson not found!",
                    StatusCode = 404
                };


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



            lesson.Name = request.Name;
            lesson.Video = "/LessonVideos/" + fileName;
            lesson.CourseId = request.CourseId;

            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Lesson successfully updated"
            };
        }
    }
}
