using AutoMapper;
using FilmesApi.Data.DTOs;
using FilmesApi.Models;

namespace FilmesApi.Profiles;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDTO, Filme>().ReverseMap();
        CreateMap<UpdateFilmeDTO, Filme>().ReverseMap();
        CreateMap<Filme, ReadFilmeDTO>().ReverseMap();
        CreateMap<Filme, ReadFilmeDTO>()
           .ForMember(filmeDto => filmeDto.Sessoes,
                   opt => opt.MapFrom(filme => filme.Sessoes)).ReverseMap();
    }
}