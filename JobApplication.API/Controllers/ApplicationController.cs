using JobApplication.Application.Features.Applications.Commands.CancelApplication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id, [FromQuery] int requesterId)
        {
            try
            {
                await _mediator.Send(new CancelApplicationCommand
                {
                    ApplicationId = id,
                    RequesterId = requesterId
                });

                return Ok(new
                {
                    message = "Application cancelled successfully."
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
