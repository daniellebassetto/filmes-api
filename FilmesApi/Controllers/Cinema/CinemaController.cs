using AutoMapper;
using FilmesApi.Data.DTOs;
using FilmesApi.Data;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public CinemaController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um cinema ao banco de dados.
        /// </summary>
        /// <param name="cinemaDTO">Parâmetro obrigatório do objeto completo informando os campos necessários para criação de um cinema</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDTO cinemaDTO)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDTO);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = cinema.Id }, cinemaDTO);
        }

        /// <summary>
        /// Consulta os cinemas cadastrados, caso o id do endereço seja informado, ele filtrará
        /// apenas os cinemas deste endereço em questão.
        /// </summary>
        /// <param name="enderecoId">Parâmetro opcional inteiro informando o id do endereço para filtragem específica</param>
        /// <returns>IEnumerable de ReadCinemaDTO</returns>
        [HttpGet]
        public IEnumerable<ReadCinemaDTO> RecuperaCinemas([FromQuery] int? enderecoId)
        {
            if(enderecoId == null)
                return _mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas.ToList());
            return _mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas.FromSqlRaw($"SELECT Id, Nome, EnderecoId FROM cinemas WHERE EnderecoId = {enderecoId}").ToList());
        }

        /// <summary>
        /// Consulta um cinema de acordo com id informado.
        /// </summary>
        /// <param name="id">Parâmetro obrigatório inteiro informando o id do cinema a ser consultado</param>
        /// <returns>IActionResult</returns>
        [HttpGet("{id}")]
        public IActionResult RecuperaCinemasPorId(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema != null)
            {
                ReadCinemaDTO cinemaDTO = _mapper.Map<ReadCinemaDTO>(cinema);
                return Ok(cinemaDTO);
            }
            return NotFound();
        }

        /// <summary>
        /// Atualiza um cinema de acordo com id informado.
        /// </summary>
        /// <param name="id">Parâmetro obrigatório inteiro informando o id do cinema a ser atualizado</param>
        /// <param name="cinemaDTO">Parâmetro obrigatório informando o objeto completo para atualização de um cinema</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDTO cinemaDTO)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }
            _mapper.Map(cinemaDTO, cinema);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deleta um cinema.
        /// </summary>
        /// <param name="id">Parâmetro obrigatório inteiro informando o id do cinema a ser deletado</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }
            _context.Remove(cinema);
            _context.SaveChanges();
            return NoContent();
        }

    }
}