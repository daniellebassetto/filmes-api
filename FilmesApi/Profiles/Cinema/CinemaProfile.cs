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
            CreateMap<Cinema, ReadCinemaDTO>()
                .ForMember(cinemaDTO => cinemaDTO.Endereco,
                    opt => opt.MapFrom(cinema => cinema.Endereco)).ReverseMap()
                .ForMember(cinemaDto => cinemaDto.Sessoes,
                    opt => opt.MapFrom(cinema => cinema.Sessoes)).ReverseMap();
            CreateMap<UpdateCinemaDTO, Cinema>().ReverseMap();
        }
    }
}