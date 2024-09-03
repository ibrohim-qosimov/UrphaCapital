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
using UrphaCapital.Application.UseCases.Homework.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Homework.Handlers
{
    public class UpdateHomeworkCommandHandler : IRequestHandler<UpdateHomeworkCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UpdateHomeworkCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResponseModel> Handle(UpdateHomeworkCommand request, CancellationToken cancellationToken)
        {
            var hw = await _context.Homeworks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (hw == null)
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Homework not found!",
                    StatusCode = 404
                };


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



            hw.Title = request.Title;
            hw.FILE = "/HomeworkFile/" + fileName;
            hw.Description = request.Description;
            hw.studentId = request.studentId;
            hw.LessonId = request.LessonId;

            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Hw successfully updated"
            };
        }
    }
}
