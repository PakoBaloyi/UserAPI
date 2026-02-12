using UserApi.Application.Common;
using UserApi.Shared.DTO;

namespace UserApi.Application.Interfaces
{
    public interface IGroupService
    {
        Task<ServiceResult<IEnumerable<GroupDto>>> GetAllGroupsAsync();
    }
}