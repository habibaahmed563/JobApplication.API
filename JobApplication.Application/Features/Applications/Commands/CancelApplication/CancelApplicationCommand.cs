using MediatR;

namespace JobApplication.Application.Features.Applications.Commands.CancelApplication
{
    public class CancelApplicationCommand : IRequest
    {
        public int ApplicationId { get; set; }
        public int RequesterId { get; set; }
    }
}
