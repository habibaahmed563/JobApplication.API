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

        public async Task<IEnumerable<JobDto>> GetAllAsync()
        {
            var jobs = await _jobRepository.GetAllAsync();

            return jobs.Select(j => new JobDto
            {
                Id = j.Id,
                Title = j.Title,
                Description = j.Descrption,
                IsActive = j.IsActive,
                RecruiterId = j.RecruiterId,
                ClosedAt = j.ClosedAt,
                ClosedBy = j.ClosedBy
            });
        }

        public async Task<JobDto?> GetByIdAsync(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);

            if (job == null)
                return null;

            return new JobDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Descrption,
                IsActive = job.IsActive,
                RecruiterId = job.RecruiterId,
                ClosedAt = job.ClosedAt,
                ClosedBy = job.ClosedBy
            };
        }

    }
}
