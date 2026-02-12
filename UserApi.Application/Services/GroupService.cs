using UserApi.Application.Common;
using UserApi.Application.Interfaces;
using UserApi.Infrastructure.Interfaces;
using UserApi.Shared.DTO;

namespace UserApi.Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _repository;

        public GroupService(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult<IEnumerable<GroupDto>>> GetAllGroupsAsync()
        {
            var groups = await _repository.GetAllAsync();
            return ServiceResult<IEnumerable<GroupDto>>.Ok(groups);
        }
    }

}