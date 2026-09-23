using MediatR;

namespace JobApplication.Application.Features.Jobs.Commands.CloseJob
{
    /// <summary>
    /// Command to close a job posting. Only the recruiter who owns the job can close it.
    /// </summary>
    public class CloseJobCommand : IRequest
    {
        /// <summary>The ID of the job to close.</summary>
        public int JobId { get; set; }

        /// <summary>The ID of the user requesting the close (must match the job's RecruiterId).</summary>
        public int RequesterId { get; set; }
    }
}
