using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.AnnouncementCRUD.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.AnnouncementCRUD.Handlers
{
    public class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAnnouncementCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
        {

            var announcement = await _context.Announcements.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (announcement != null)
            {
                if (request.Title != null)
                {
                    announcement.Title = request.Title;
                }

                _context.Announcements.Update(announcement);
                await _context.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    Message = "Announcement updated successfully",
                    StatusCode = 200,
                    IsSuccess = true
                };
            }

            return new ResponseModel()
            {
                Message = "Error while updating!",
                StatusCode = 401
            };
        }
    }
}
