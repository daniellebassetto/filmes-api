using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;

public class Endereco
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string Logradouro { get; set; }
    public int Numero { get; set; }
    // criando uma relação 1:1 com o cinema para que o entity possa entender
    public virtual Cinema Cinema { get; set; }
}