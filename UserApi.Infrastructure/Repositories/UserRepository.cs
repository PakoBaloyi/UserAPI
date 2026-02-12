using Microsoft.EntityFrameworkCore;
using UserApi.Domain.Entities;
using UserApi.Infrastructure.Data;
using UserApi.Infrastructure.Interfaces;

namespace UserApi.Infrastructure.Repositories
{
    public class UserRepository(UserDbContext context) : IUserRepository
    {
        private readonly UserDbContext _context = context;

        public async Task<User?> GetByIdAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Groups)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _context.Users
                .Include(u => u.Groups)
                .ToListAsync();

            return users;
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetUserCountAsync()
        {
            var count = await _context.Users.CountAsync();
            return count;
        }

        public async Task<IDictionary<string, int>> GetUserCountsPerGroupAsync()
        {
            var groupCounts = await _context.Groups
                .Select(g => new { g.Name, UserCount = g.Users.Count })
                .ToDictionaryAsync(x => x.Name, x => x.UserCount);

            return groupCounts;
        }
    }
}
