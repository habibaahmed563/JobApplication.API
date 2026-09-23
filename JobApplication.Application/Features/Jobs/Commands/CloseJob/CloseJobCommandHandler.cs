using JobApplication.Application.Interfaces;
using MediatR;

namespace JobApplication.Application.Features.Jobs.Commands.CloseJob
{
    /// <summary>
    /// Handles the <see cref="CloseJobCommand"/> by validating ownership and status,
    /// then marking the job as closed.
    /// </summary>
    public class CloseJobCommandHandler : IRequestHandler<CloseJobCommand>
    {
        private readonly IJobRepository _jobRepository;

        public CloseJobCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task Handle(CloseJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);

            if (job == null)
            {
                throw new KeyNotFoundException("Job not found.");
            }

            if (job.RecruiterId != request.RequesterId)
            {
                throw new UnauthorizedAccessException(
                    "You are not allowed to close this job."
                );
            }

            if (job.ClosedAt != null)
            {
                throw new InvalidOperationException(
                    "Job is already closed."
                );
            }

            job.ClosedAt = DateTime.UtcNow;
            job.ClosedBy = request.RequesterId;

            _jobRepository.Update(job);

            await _jobRepository.SaveChangesAsync();
        }
    }
}
