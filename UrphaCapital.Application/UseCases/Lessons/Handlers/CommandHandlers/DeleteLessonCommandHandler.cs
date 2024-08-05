using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Lessons.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Lessons.Handlers.CommandHandlers
{
    public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;

        public DeleteLessonCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {

            var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (lesson == null)
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Lesson not found!",
                    StatusCode = 404
                };

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Lesson successfully deleted"
            };
        }
    }
}
