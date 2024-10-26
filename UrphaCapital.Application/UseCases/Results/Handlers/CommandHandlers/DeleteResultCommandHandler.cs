using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Results.Commands;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Results.Handlers.CommandHandlers
{
    public class DeleteResultCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment) : IRequestHandler<DeleteResultCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<ResponseModel> Handle(DeleteResultCommand request, CancellationToken cancellationToken)
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

            var relativePath = result.PictureUrl.TrimStart('/');
            var deleteFilePath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath);

            if (File.Exists(deleteFilePath))
                File.Delete(deleteFilePath);

            _context.Results.Remove(result);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                IsSuccess = true,
                Message = "Deleted",
                StatusCode = 201,
            };
        }
    }
}
