using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.StudentsCRUD.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Lessons.Handlers.CommandHandlers
{
    public class AddMyCourseCommandHandler : IRequestHandler<AddMyCourseCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;

        public AddMyCourseCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(AddMyCourseCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == request.StudentId);

            if (student == null)
            {
                return new ResponseModel()
                {
                    IsSuccess = false,
                    StatusCode = 404,
                    Message = "Student not found!"
                };
            }

            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == request.CourseId);

            if (course == null)
            {
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Course not found",
                    StatusCode = 404
                };
            }

            student.CourseIds.Add(course.Id);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                IsSuccess = true,
                Message = "Successfully added",
                StatusCode = 200
            };
        }
    }
}
