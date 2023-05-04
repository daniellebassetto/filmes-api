using AutoMapper;
using FilmesApi.Data.DTOs;
using FilmesApi.Models;

namespace FilmesApi.Profiles
{
    public class EnderecoProfile : Profile
    {
        public EnderecoProfile()
        {
            CreateMap<CreateEnderecoDTO, Endereco>().ReverseMap();
            CreateMap<Endereco, ReadEnderecoDTO>().ReverseMap();
            CreateMap<UpdateEnderecoDTO, Endereco>().ReverseMap();
        }
    }
}