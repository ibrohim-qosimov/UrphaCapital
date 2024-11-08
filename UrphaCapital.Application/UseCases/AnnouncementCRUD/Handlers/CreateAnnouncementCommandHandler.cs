using MediatR;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.AnnouncementCRUD.Commands;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.AnnouncementCRUD.Handlers
{
    public class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;

        public CreateAnnouncementCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var announcement = new Announcement
            {
                Title = request.Title
            };

            await _context.Announcements.AddAsync(announcement, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                Message = "Announcement created successfully.",
                StatusCode = 200,
                IsSuccess = true
            };
        }
    }
}
