using AutoMapper;
using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.Models;

namespace Hahn.ApplicationProcess.February2021.Domain
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Asset, AssetDto>()
                .ForMember(dest => dest.Deparment, opt => opt.MapFrom(src => src.DepartmentId))
                .ReverseMap();
        }
    }
}
