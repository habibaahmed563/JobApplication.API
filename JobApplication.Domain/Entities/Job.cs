using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Domain.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descrption { get; set; }
        public bool IsActive { get; set; }

        public int RecruiterId { get; set; }
        public DateTime? ClosedAt { get; set; }
        public int? ClosedBy { get; set; }
    }
}
