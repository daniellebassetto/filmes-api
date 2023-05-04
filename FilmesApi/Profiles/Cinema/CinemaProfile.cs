using AutoMapper;
using FilmesApi.Data.DTOs;
using FilmesApi.Models;

namespace FilmesApi.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaDTO, Cinema>().ReverseMap();
            // Para meu campo de ReadEnderecoDTO quero pegar da minha origem o campo de endereço
            CreateMap<Cinema, ReadCinemaDTO>()
               .ForMember(cinemaDto => cinemaDto.Endereco,
                   opt => opt.MapFrom(cinema => cinema.Endereco)).ReverseMap();
            CreateMap<UpdateCinemaDTO, Cinema>().ReverseMap();
        }
    }
}