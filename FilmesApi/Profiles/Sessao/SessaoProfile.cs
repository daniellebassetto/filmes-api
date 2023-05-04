using AutoMapper;
using FilmesApi.Data.DTOs;
using FilmesApi.Models;

namespace FilmesApi.Profiles
{
    public class SessaoProfile : Profile
    {
        public SessaoProfile()
        {
            CreateMap<CreateSessaoDTO, Sessao>().ReverseMap();
            CreateMap<Sessao, ReadSessaoDTO>().ReverseMap();
        }
    }
}