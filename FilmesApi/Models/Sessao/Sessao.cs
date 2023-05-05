using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models
{
    public class Sessao
    {
        public int? FilmeId { get; set; }
        // filme indicando a relação 1:N de sessões e filmes
        public virtual Filme Filme { get; set; }
        public int? CinemaId { get; set; }
        // uma sessão específica está em apenas um cinema
        public virtual Cinema Cinema { get; set; }
    }
}