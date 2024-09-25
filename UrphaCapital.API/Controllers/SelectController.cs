using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrphaCapital.Application.UseCases.Selects.Queries;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SelectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SelectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-mentors")]
        public async Task<IActionResult> GetMentors()
        {
            var query = new SelectAllMentorsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("get-students")]
        public async Task<IActionResult> GetStudents()
        {
            var query = new SelectAllStudentsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("get-courses")]
        public async Task<IActionResult> GetCourses()
        {
            var query = new SelectAllCoursesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
