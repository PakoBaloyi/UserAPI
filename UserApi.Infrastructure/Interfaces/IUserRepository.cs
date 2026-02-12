using UserApi.Application.DTO;
using UserApi.Domain.Entities;

namespace UserApi.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<int> GetUserCountAsync();
        Task<IDictionary<string, int>> GetUserCountsPerGroupAsync();

    }
}