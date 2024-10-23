using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Results.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Results.Handlers.CommandHandlers
{
    public class UpdateResultCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment) : IRequestHandler<UpdateResultCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<ResponseModel> Handle(UpdateResultCommand request, CancellationToken cancellationToken)
        {
            var result = await _context.Results.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

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
                var deletedFilePath = Path.Combine("wwwroot", _webHostEnvironment.WebRootPath, result.PictureUrl);

                if (File.Exists(deletedFilePath))
                    File.Delete(deletedFilePath);

                var file = request.Picture;
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "ResultPhotos");
                string fileName = "";

                try
                {
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                        Debug.WriteLine("Directory created successfully.");
                    }

                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    filePath = Path.Combine(_webHostEnvironment.WebRootPath, "ResultPhotos", fileName);
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
