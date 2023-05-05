using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.DTOs
{
    public class CreateSessaoDTO
    {
        [Required]
        public int FilmeId { get; set; }
        public int CinemaId { get; set; }
    }
}