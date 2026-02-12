using Microsoft.EntityFrameworkCore;
using UserApi.Domain.Entities;
using UserApi.Infrastructure.Data;
using UserApi.Infrastructure.Interfaces;

namespace UserApi.Infrastructure.Repositories
{
    public class GroupRepository (UserDbContext context)  : IGroupRepository
    {
        private readonly UserDbContext _context = context;
        public async Task<IEnumerable<Group>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Groups
                .Where(g => ids.Contains(g.Id))
                .ToListAsync();
        }
    }
}
