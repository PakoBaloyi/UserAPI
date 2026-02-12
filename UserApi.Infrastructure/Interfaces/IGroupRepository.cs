using UserApi.Domain.Entities;
using UserApi.Shared.DTO;

namespace UserApi.Infrastructure.Interfaces
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetByIdsAsync(IEnumerable<int> ids);
        Task<IEnumerable<GroupDto>> GetAllAsync();

    }
}
