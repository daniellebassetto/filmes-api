using AutoMapper;
using FilmesApi.Data.DTOs;
using FilmesApi.Data;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public SessaoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Adiciona uma sessão ao banco de dados.
        /// </summary>
        /// <param name="createSessaoDTO">Parâmetro obrigatório do objeto completo informando os campos necessários para criação de uma sessão</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaSessao(CreateSessaoDTO createSessaoDTO)
        {
            Sessao sessao = _mapper.Map<Sessao>(createSessaoDTO);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaSessoesPorId), new { filmeId = sessao.FilmeId, cinemaId = sessao.CinemaId }, sessao);
        }

        /// <summary>
        /// Consulta as sessões cadastradas.
        /// </summary>
        /// <returns>IEnumerable de ReadSessaoDTO</returns>
        [HttpGet]
        public IEnumerable<ReadSessaoDTO> RecuperaSessoes()
        {
            return _mapper.Map<List<ReadSessaoDTO>>(_context.Sessoes.ToList());
        }

        /// <summary>
        /// Consulta uma sessão de acordo com id do filme e do cinema informado (chave composta).
        /// </summary>
        /// <param name="filmeId">Parâmetro obrigatório inteiro informando o id do filme</param>
        /// <param name="cinemaId">Parâmetro obrigatório inteiro informando o id do cinema</param>
        /// <returns>IActionResult</returns>
        [HttpGet("{filmeId}/{cinemaId}")]
        public IActionResult RecuperaSessoesPorId(int filmeId, int cinemaId)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.FilmeId == filmeId && sessao.CinemaId == cinemaId);
            if (sessao != null)
            {
                ReadSessaoDTO sessaoDTO = _mapper.Map<ReadSessaoDTO>(sessao);

                return Ok(sessaoDTO);
            }
            return NotFound();
        }
    }
}