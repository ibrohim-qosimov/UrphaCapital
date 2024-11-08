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
using UrphaCapital.Application.UseCases.IdeasCrud.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.IdeasCrud.Handlers
{
    public class UpdateIdeaCommandHandler : IRequestHandler<UpdateIdeaCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UpdateIdeaCommandHandler(IWebHostEnvironment webHostEnvironment, IApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task<ResponseModel> Handle(UpdateIdeaCommand request, CancellationToken cancellationToken)
        {
            var result = await _context.Ideass.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (result == null)
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
                var relativePath = result.PictureUrl.TrimStart('/');
                var deleteFilePath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath);

                if (File.Exists(deleteFilePath))
                    File.Delete(deleteFilePath);

                var file = request.Picture;
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "IdeaPhotos");
                string fileName = "";

                try
                {
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                        Debug.WriteLine("Directory created successfully.");
                    }

                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    filePath = Path.Combine(_webHostEnvironment.WebRootPath, "IdeaPhotos", fileName);
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

                result.PictureUrl = "/ResultPhotos/" + fileName;
            }


            if (request.Title != null)
                result.Title = request.Title;

            if (request.Description != null)
                result.Description = request.Description;

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
