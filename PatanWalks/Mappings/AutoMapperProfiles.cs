using AutoMapper;
using PatanWalks.Models.Domain;
using PatanWalks.Models.DTO;

namespace PatanWalks.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Division, DivisionGetDTO>().ReverseMap();
            CreateMap<District, DistrictPostDTO>().ReverseMap();
            CreateMap<District, DistrictDTO>().ReverseMap();
        }
    }
}
