using JobApplication.Application.Dtos;
using JobApplication.Application.Features.Jobs.Commands.CloseJob;
using JobApplication.Application.Features.Jobs.Commands.CreateJob;
using JobApplication.Application.Services;
using MediatR;
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
        private readonly IMediator _mediator;

        public JobController(JobService jobService, IMediator mediator)
        {
            _jobService = jobService;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateJobDto createJobDto)
        {
            var id = await _mediator.Send(new CreateJobCommand
            {
                Title = createJobDto.Title,
                Description = createJobDto.Description,
            });
            return Ok(new
            {
                id = id
            });
        }

        [Authorize]
        [HttpPut("{id}/close")]
        /// <summary>
        /// Closes an active job posting. Only the recruiter who created the job can close it.
        /// </summary>
        /// <param name="id">The ID of the job to close.</param>
        /// <returns>A success message or an error response.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Close(int id)
        {
            try
            {
                var requesterId = int.Parse(
                    User.FindFirst("UserId")!.Value
                );

                await _mediator.Send(new CloseJobCommand
                {
                    JobId = id,
                    RequesterId = requesterId
                });

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


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobs = await _jobService.GetAllAsync();
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var job = await _jobService.GetByIdAsync(id);

            if (job == null)
            {
                return NotFound(new
                {
                    message = $"Job with id {id} not found."
                });
            }

            return Ok(job);
        }




    }
}
