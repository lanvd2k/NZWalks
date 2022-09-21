using AutoMapper;

using NZWalks.Web.Models.DTO;

namespace NZWalks.API.Profiles
{
    public class WalksProfile : Profile
    {
        public WalksProfile()
        {
            CreateMap<AddWalkRequest, WalkDTO>().ReverseMap();
            CreateMap<UpdateWalkRequest, WalkDTO>().ReverseMap();
            CreateMap<AddWalkDifficultyRequest, WalkDifficultyDTO>().ReverseMap();
            CreateMap<UpdateWalkDifficultyRequest, WalkDifficultyDTO>().ReverseMap();
        }
    }
}
