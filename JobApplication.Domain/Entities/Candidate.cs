using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Domain.Entities
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CvUrl { get; set; }
    }
}
