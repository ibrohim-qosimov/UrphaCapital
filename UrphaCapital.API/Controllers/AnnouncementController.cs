using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrphaCapital.Application.UseCases.AnnouncementCRUD.Commands;
using UrphaCapital.Application.UseCases.AnnouncementCRUD.Queries;
using UrphaCapital.Application.UseCases.IdeasCrud.Commands;
using UrphaCapital.Application.UseCases.IdeasCrud.Queries;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnnouncementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostAnnouncement(CreateAnnouncementCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutAnnouncement(UpdateAnnouncementCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(long id)
        {
            var command = new DeleteAnnouncementCommand { Id = id };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAnnouncements()
        {
            var query = new GetAllAnnouncementsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{announcementId}")]
        public async Task<IActionResult> GetAnnouncements(long announcementId)
        {
            var query = new GetAnnouncementByIdQuery()
            {
                Id = announcementId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
