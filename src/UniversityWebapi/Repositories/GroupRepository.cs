using Microsoft.EntityFrameworkCore;
using UniversityWebapi.Database;

namespace UniversityWebapi.Repositories
{
    public class GroupRepository(UniversityDbContext dbContext)
    {
        readonly UniversityDbContext _dbContext = dbContext;
    
        public async Task CreateAsync(Models.Group group)
        {
            await _dbContext.Groups.AddAsync(group);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Models.Group> GetByIdAsync(int id)
        {
            return await _dbContext.Groups
                .Include(g => g.Major)
                .ThenInclude(m => m.Faculty)
                .FirstOrDefaultAsync(g => g.Id == id)
                ?? throw new KeyNotFoundException($"Group with ID {id} not found.");
        }

        public async Task<List<Models.Group>> GetAllAsync()
        {
            return await _dbContext.Groups
                .Include(g => g.Major)
                .ThenInclude(m => m.Faculty)
                .ToListAsync();
        }
    }
}