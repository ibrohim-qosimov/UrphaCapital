using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Courses.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Courses.Handlers.CommandHandlers
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UpdateCourseCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResponseModel> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
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

            File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, course.Picture));

            var file = request.Picture;
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "CoursesPictures");
            string fileName = "";

            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    Debug.WriteLine("Directory created successfully.");
                }

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(_webHostEnvironment.WebRootPath, "CoursesPictures", fileName);
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

            course.Name = request.Name;
            course.Description = request.Description;
            course.Subtitle = request.Subtitle;
            course.Picture = "/CoursesPictures/" + fileName;
            course.Price = request.Price;
            course.MentorId = request.MentorId;

            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                IsSuccess = true,
                Message = "Updated",
                StatusCode = 201,
            };
        }
    }
}
