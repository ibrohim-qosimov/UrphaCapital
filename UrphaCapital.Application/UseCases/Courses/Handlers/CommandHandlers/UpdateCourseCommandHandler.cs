using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Courses.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Courses.Handlers.CommandHandlers;
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

        if (request.Picture != null)
        {
            var relativePath = course.Picture.TrimStart('/');
            var deleteFilePath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath);

            if (File.Exists(deleteFilePath))
                File.Delete(deleteFilePath);

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
            course.Picture = "/CoursesPictures/" + fileName;
        }

        if (request.Name != null)
            course.Name = request.Name;

        if (request.Description != null)
            course.Description = request.Description;

        if (request.Subtitle != null)
            course.Subtitle = request.Subtitle;

        if (request.Price != null)
            course.Price = (decimal)request.Price;

        if (request.MentorId != null)
            course.MentorId = (long)request.MentorId;

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseModel()
        {
            IsSuccess = true,
            Message = "Updated",
            StatusCode = 201,
        };
    }
}
