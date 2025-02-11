using AutoMapper;
using KcalIntakeTracker_KIT.Models;
using KcalIntakeTracker_KIT.Dto;

namespace KcalIntakeTracker_KIT.Helper
{ 

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<DailyLog, DailyLogDto>().ReverseMap();
            CreateMap<FoodItem, FoodItemDto>().ReverseMap();
        }
    } 
}
