using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Mentors.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Mentors.Handlers.CommandHandlers
{
    public class DeleteMentorCommandHandler : IRequestHandler<DeleteMentorCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeleteMentorCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResponseModel> Handle(DeleteMentorCommand request, CancellationToken cancellationToken)
        {
            var mentor = await _context.Mentors.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (mentor == null)
            {
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Not found",
                    StatusCode = 404,
                };
            }

            File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, mentor.Picture));

            _context.Mentors.Remove(mentor);
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
