using JobApplication.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace JobApplication.Infrastructure.Services
{
    public class EmailNotificationService : INotificationService
    {
        private readonly ILogger<EmailNotificationService> _logger;

        public EmailNotificationService(ILogger<EmailNotificationService> logger)
        {
            _logger = logger;
        }

        public async Task NotifyCandidate(int applicationId)
        {
            _logger.LogInformation("Notifying candidate for Application #{ApplicationId}...", applicationId);
            // Simulate sending email/notification
            await Task.Delay(300);
            _logger.LogInformation("Notification successfully sent to candidate for Application #{ApplicationId}.", applicationId);
        }
    }
}
