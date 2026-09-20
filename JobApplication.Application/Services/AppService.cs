using JobApplication.Application.Interfaces;
using JobApplication.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Services
{
    public class AppService
    {
        public class ApplicationService
        {
            private readonly IAppRepository _appRepository;

            public ApplicationService(IAppRepository appRepository)
            {
                _appRepository = appRepository;
            }

            public async Task CancelAsync(int id, int requesterId)
            {
                var application = await _appRepository.GetByIdAsync(id);

                if (application == null)
                {
                    throw new KeyNotFoundException("Application not found.");
                }

                if (application.UserId != requesterId)
                {
                    throw new UnauthorizedAccessException(
                        "You are not allowed to cancel this application."
                    );
                }

                if (application.JobApplicationStatus == JobApplicationStatus.InterView)
                {
                    throw new InvalidOperationException(
                        "Application cannot be cancelled during interview."
                    );
                }

                application.JobApplicationStatus = JobApplicationStatus.Cancelled;
                application.CancelledAt = DateTime.UtcNow;

                _appRepository.Update(application);

                await _appRepository.SaveChangesAsync();
            }


        }
    }
}
