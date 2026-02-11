using UserApi.Application.Common;
using UserApi.Application.DTO;

namespace UserApi.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<UserDto>> GetUserAsync(int id);
        Task<ServiceResult<IEnumerable<UserDto>>> GetAllUsersAsync();
        Task<ServiceResult<UserDto>> AddUserAsync(CreateUserDto dto);
        Task<ServiceResult<bool>> UpdateUserAsync(UpdateUserDto dto);
        Task<ServiceResult<bool>> DeleteUserAsync(int id);
        Task<ServiceResult<int>> GetUserCountAsync();
        Task<ServiceResult<IDictionary<string, int>>> GetUserCountsPerGroupAsync();
    }
}
