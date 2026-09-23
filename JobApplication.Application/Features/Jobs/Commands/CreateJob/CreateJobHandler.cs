using JobApplication.Application.Dtos;
using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Features.Jobs.Commands.CreateJob
{
    public class CreateJobHandler : IRequestHandler<CreateJobCommand, int>
    {
        private readonly IJobRepository _jobRepository;

        public CreateJobHandler(Interfaces.IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<int> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var job = new Job()
            {
                Title = request.Title,
                Descrption = request.Description,
                IsActive = true
            };

            await _jobRepository.InsertAsync(job);
            await _jobRepository.SaveChangesAsync();
            return job.Id;
        }

    }
}
