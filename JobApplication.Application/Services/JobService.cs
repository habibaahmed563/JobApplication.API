using JobApplication.Application.Dtos;
using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Services
{
    public class JobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<int> CreateAsync(CreateJobDto createJobDto)
        {
            var job = new Job()
            {
                Title = createJobDto.Title,
                Descrption = createJobDto.Description,
                IsActive = true
            };

            await _jobRepository.InsertAsync(job);
            await _jobRepository.SaveChangesAsync();
            return job.Id;
        }

        public async Task CloseAsync(int jobId, int requesterId)
        {
            var job = await _jobRepository.GetByIdAsync(jobId);

            if (job == null)
            {
                throw new KeyNotFoundException("Job not found.");
            }

            if (job.RecruiterId != requesterId)
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
            job.ClosedBy = requesterId;

            _jobRepository.Update(job);

            await _jobRepository.SaveChangesAsync();
        }

    }
}
