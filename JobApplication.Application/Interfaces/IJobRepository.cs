using JobApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Interfaces
{
    public interface IJobRepository
    {
         Task InsertAsync(Job job);

         void Update(Job job);

         IQueryable<Job> Get(Job job);

         void Remove(Job job);

         Task SaveChangesAsync();

         Task<Job?> GetByIdAsync(int id);

         Task<IEnumerable<Job>> GetAllAsync();
    }
}
