using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Interfaces
{
    public interface IAppRepository
    {
         Task<Domain.Entities.Application?> GetByIdAsync(int id);


         void Update(Domain.Entities.Application application);

         Task SaveChangesAsync();

    }
}
