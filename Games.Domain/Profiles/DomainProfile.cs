using AutoMapper;
using Games.Domain.Dtos;
using Games.Domain.Models;

namespace Games.Domain.Profiles;

public class DomainProfile : Profile
{
    public DomainProfile()
    {
        CreateMap<Game, GameDto>()
            .ForMember(d => d.GameGenres, s => s
                .MapFrom(src => src.GameGenres.Select(x => x.Title)));
    }
}