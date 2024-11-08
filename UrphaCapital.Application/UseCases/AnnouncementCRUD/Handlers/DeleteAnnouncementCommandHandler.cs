using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.AnnouncementCRUD.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.AnnouncementCRUD.Handlers
{
    public class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteAnnouncementCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAnnouncementCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var announcement = await _context.Announcements.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (announcement == null)
                {
                    return new ResponseModel
                    {
                        Message = "Announcement not found",
                        StatusCode = 404,
                        IsSuccess = false
                    };
                }

                _context.Announcements.Remove(announcement);
                await _context.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    Message = "Announcement deleted successfully",
                    StatusCode = 200,
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Message = "An error occurred while deleting the announcement",
                    StatusCode = 500,
                    IsSuccess = false
                };
            }
        }
    }
}
