using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;

public class Cinema
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "O campo de nome é obrigatório.")]
    public string Nome { get; set; }
    // dentro do cinema, temos que ter o Id do endereço, pois todo cinema tem um endereço
    public int EnderecoId { get; set; }
    // um cinema pode ter apenas um endereco (1:1)
    public virtual Endereco Endereco { get; set; }
    // um cinema pode ter várias sessões
    public virtual ICollection<Sessao> Sessoes { get; set; }
}