using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static JobApplication.Application.Services.AppService;

namespace JobApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationService _applicationService;

        public ApplicationController(ApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id, [FromQuery] int requesterId)
        {
            try {
                await _applicationService.CancelAsync(id, requesterId);
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
