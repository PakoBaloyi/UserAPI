using AutoMapper;
using UserApi.Application.DTO;
using UserApi.Domain.Entities;

namespace UserApi.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => $"{src.Name} {src.LastName}"))
                .ForMember(dest => dest.Groups,
                           opt => opt.MapFrom(src => src.Groups.Select(g => g.Name)));
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }

}