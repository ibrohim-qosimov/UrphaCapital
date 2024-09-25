using MediatR;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Mentors.Commands
{
    public class DeleteMentorCommand : IRequest<ResponseModel>
    {
        public long Id { get; set; }
    }
}
