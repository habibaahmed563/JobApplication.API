using JobApplication.Application.Interfaces;
using JobApplication.Domain.Enums;
using MediatR;

namespace JobApplication.Application.Features.Applications.Commands.CancelApplication
{
    public class CancelApplicationCommandHandler : IRequestHandler<CancelApplicationCommand>
    {
        private readonly IAppRepository _appRepository;

        public CancelApplicationCommandHandler(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public async Task Handle(CancelApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = await _appRepository.GetByIdAsync(request.ApplicationId);

            if (application == null)
            {
                throw new KeyNotFoundException("Application not found.");
            }

            if (application.UserId != request.RequesterId)
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
