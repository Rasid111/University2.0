using Microsoft.EntityFrameworkCore;
using UniversityWebapi.Database;
using UniversityWebapi.Models;

namespace UniversityWebapi.Repositories
{
    public class MajorRepository(UniversityDbContext dbContext)
    {
        private readonly UniversityDbContext _dbContext = dbContext;

        public async Task CreateAsync(Major major)
        {
            await _dbContext.Majors.AddAsync(major);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Major?> GetByIdAsync(int id)
        {
            return await _dbContext.Majors
                .Include(m => m.Faculty)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Major>> GetAllAsync()
        {
            return await _dbContext.Majors
                .Include(m => m.Faculty)
                .ToListAsync();
        }
    }
}