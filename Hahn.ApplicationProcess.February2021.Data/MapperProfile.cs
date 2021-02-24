using AutoMapper;
using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using System;

namespace Hahn.ApplicationProcess.February2021.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Asset, AssetDto>()
                .ForMember(dest => dest.Deparment, opt => opt.MapFrom(src => src.DepartmentId));

            CreateMap<AssetDto, Asset>()
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Deparment))
            .ForMember(dest => dest.Deparment, opt => opt.Ignore());
        }
    }
}
