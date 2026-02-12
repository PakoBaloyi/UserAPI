using UserApi.Domain.Entities;

namespace UserApi.Infrastructure.Interfaces
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetByIdsAsync(IEnumerable<int> ids);

    }
}
