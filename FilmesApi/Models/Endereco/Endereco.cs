using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;

public class Endereco
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string Logradouro { get; set; }
    public int Numero { get; set; }
    // em um endereco é possível existir apenas um cinema (1:1), ou nenhum, por isso
    // não precisamos do Id do Cinema
    public virtual Cinema Cinema { get; set; }
}