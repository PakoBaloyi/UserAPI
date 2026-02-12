using AutoMapper;
using UserApi.Application.Common;
using UserApi.Application.DTO;
using UserApi.Application.Interfaces;
using UserApi.Domain.Entities;
using UserApi.Infrastructure.Interfaces;

namespace UserApi.Application.Services
{
    public class UserService(IUserRepository repository, IMapper mapper, IGroupRepository groupRepository) : IUserService
    {
        private readonly IUserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task<ServiceResult<UserDto>> GetUserAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                return ServiceResult<UserDto>.Fail("User not found");

            return ServiceResult<UserDto>.Ok(_mapper.Map<UserDto>(user));
        }

        public async Task<ServiceResult<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllAsync();
            return ServiceResult<IEnumerable<UserDto>>.Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        public async Task<ServiceResult<UserDto>> AddUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Name = dto.Name,
                LastName = dto.LastName,
                IsActive = true
            };

            if (dto.GroupIds.Count != 0)
            {
                var groups = await _groupRepository.GetByIdsAsync(dto.GroupIds);
                user.Groups = groups.ToList();
            }

            await _repository.AddAsync(user);

            return ServiceResult<UserDto>.Ok(_mapper.Map<UserDto>(user));
        }



        public async Task<ServiceResult<bool>> UpdateUserAsync(UpdateUserDto dto)
        {
            var user = await _repository.GetByIdAsync(dto.Id);
            if (user == null)
                return ServiceResult<bool>.Fail("User not found");

            _mapper.Map(dto, user);

            if (dto.GroupIds != null && dto.GroupIds.Count != 0)
            {
                user.Groups.Clear();

                foreach (var groupId in dto.GroupIds)
                {
                    var groups = await _groupRepository.GetByIdsAsync([groupId]);
                    var group = groups.FirstOrDefault();
                    if (group != null)
                    {
                        user.Groups.Add(group);
                    }
                }
            }

            await _repository.UpdateAsync(user);
            return ServiceResult<bool>.Ok(true);
        }

        public async Task<ServiceResult<bool>> DeleteUserAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                return ServiceResult<bool>.Fail("User not found");

            user.IsActive = false; 
            await _repository.UpdateAsync(user);
            return ServiceResult<bool>.Ok(true);
        }

        public async Task<ServiceResult<int>> GetUserCountAsync()
        {
            var count = await _repository.GetUserCountAsync();
            return ServiceResult<int>.Ok(count);
        }

        public async Task<ServiceResult<IDictionary<string, int>>> GetUserCountsPerGroupAsync()
        {
            var counts = await _repository.GetUserCountsPerGroupAsync();
            return ServiceResult<IDictionary<string, int>>.Ok(counts);
        }
    }
}