using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;

public class Cinema
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    public string Nome { get; set; }
    // chave da tabela estrangeira
    public int EnderecoId { get; set; }
    // criando uma relação 1:1 com o cinema para que o entity possa entender
    public virtual Endereco Endereco { get; set; }
}