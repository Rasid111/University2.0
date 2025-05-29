using Microsoft.EntityFrameworkCore;
using UniversityWebapi.Database;
using UniversityWebapi.Models;

namespace UniversityWebapi.Repositories
{
    public class JwtTokenRepository(UniversityDbContext dbContext)
    {
        readonly UniversityDbContext _dbContext = dbContext;
        public async Task CreateRefreshToken(RefreshToken token)
        {
            await _dbContext.RefreshTokens.AddAsync(token);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<RefreshToken?> GetRefreshToken(Guid token, string userId)
        {
            return await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token && rt.UserId == userId);
        }
        public async Task DeleteRefreshToken(RefreshToken refreshToken)
        {
            _dbContext.RefreshTokens.Remove(refreshToken);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteRefreshTokensByUserId(string id)
        {
            _dbContext.RefreshTokens.RemoveRange(_dbContext.RefreshTokens.Where(rt => rt.UserId == id));
            await _dbContext.SaveChangesAsync();
        }
    }
}