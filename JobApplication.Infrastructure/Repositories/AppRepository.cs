using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Infrastructure.Repositories
{
    public class AppRepository : IAppRepository
    {
        private readonly ApplicationDbContext _context;

        public AppRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Domain.Entities.Application?> GetByIdAsync(int id)
        {
            return await _context.Applications
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Domain.Entities.Application application)
        {
            _context.Applications.Update(application);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
