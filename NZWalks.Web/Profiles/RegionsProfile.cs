using AutoMapper;

using NZWalks.Web.Models.DTO;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<AddRegionRequest, RegionDTO>().ReverseMap();
            CreateMap<UpdateRegionRequest, RegionDTO>().ReverseMap();
        }
    }
}
