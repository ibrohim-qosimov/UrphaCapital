using MediatR;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Homework.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Homework.Handlers
{
    public class GradeHomeworkCommandHandler : IRequestHandler<GradeHomeworkCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;
        public async Task<ResponseModel> Handle(GradeHomeworkCommand request, CancellationToken cancellationToken)
        {
            var homework = await _context.Homeworks.FindAsync(request.HomeworkId);
            if (homework == null) return new ResponseModel() { IsSuccess = false, Message = "Not Found", StatusCode = 404 };

            homework.Grade = request.Grade;
            homework.MentorId = request.MentorId;
            await _context.SaveChangesAsync();

            return new ResponseModel()
            {
                IsSuccess = true,
                Message = "Successfully Graded",
                StatusCode = 200
            };
        }
    }
}
