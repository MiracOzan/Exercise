using AutoMapper;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<City, CityForListDto>()
            .ForMember(d=>d.PhotoUrl,
                opt =>
                {
                    opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);
                });

        CreateMap<City, CityForDetailDto>();

    }
}   

