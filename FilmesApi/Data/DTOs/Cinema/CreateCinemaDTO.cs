using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.DTOs;

public class CreateCinemaDTO
{
    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    public string Nome { get; set; }
    public int EnderecoId { get; set; }
}