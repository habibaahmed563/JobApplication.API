using JobApplication.Application.Dtos;
using JobApplication.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly JobService _jobService;

        public JobController(JobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateJobDto createJobDto)
        {
            var id = await _jobService.CreateAsync(createJobDto);
            return Ok(new
            {
                id = id
            });
        }

        [Authorize]
        [HttpPut("{id}/close")]
        public async Task<IActionResult> Close(int id)
        {
            try
            {
                var requesterId = int.Parse(
                    User.FindFirst("UserId")!.Value
                );

                await _jobService.CloseAsync(id, requesterId);

                return Ok(new
                {
                    message = "Job closed successfully."
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
