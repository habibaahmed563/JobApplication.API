using JobApplication.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Domain.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int UserId { get; set; }
        public JobApplicationStatus JobApplicationStatus { get; set; }
        public DateTime? CancelledAt { get; set; }

    }
}
