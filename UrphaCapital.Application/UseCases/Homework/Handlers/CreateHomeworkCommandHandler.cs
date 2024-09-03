using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Homework.Commands;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Homework.Handlers
{
    public class CreateHomeworkCommandHandler : IRequestHandler<CreateHomeworkCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CreateHomeworkCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResponseModel> Handle(CreateHomeworkCommand request, CancellationToken cancellationToken)
        {
            var file = request.FILE;
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "HomeworkFile");
            string fileName = "";

            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    Debug.WriteLine("Directory created successfully.");
                }

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(_webHostEnvironment.WebRootPath, "HomeworkFile", fileName);
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

            var homework = new Homeworks()
            {
                Title = request.Title,
                LessonId = request.LessonId,
                Description = request.Description,
                studentId = request.studentId,
                FILE = "/HomeworkFile/" + fileName,
            };

            await _context.Homeworks.AddAsync(homework, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                IsSuccess = true,
                Message = "Created",
                StatusCode = 201,
            };
        }
    }
}
