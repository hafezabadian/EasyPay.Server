using AutoMapper;
using EasyPay.Common.Helper;
using EasyPay.Data.Dto.Site.Admin.Users;
using EasyPay.Data.Model;

namespace EasyPay.Presentation.Helper
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForDetailedDto>()
             .ForMember
               (dest => dest.PhotoUrl,
                opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p=> p.IsMain).Url);
                })
             .ForMember
               (dest => dest.Age,
                opt =>
                {
                    opt.MapFrom(src => src.DateOfBirth.ToAge());
                });
            CreateMap<User, UserForListDto>();
            CreateMap<Photo, PhotoForUserDto>();
            CreateMap<BankCard, BankCardForUserDto>();
        }
    }
}
