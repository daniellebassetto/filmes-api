using AutoMapper;
using FilmesApi.Data.DTOs;
using FilmesApi.Data;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public EnderecoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um endereço ao banco de dados.
        /// </summary>
        /// <param name="enderecoDTO">Parâmetro obrigatório do objeto completo informando os campos necessários para criação de um endereço</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDTO enderecoDTO)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDTO);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaEnderecosPorId), new { Id = endereco.Id }, endereco);
        }

        /// <summary>
        /// Consulta os endereços cadastrados.
        /// </summary>
        /// <returns>IEnumerable de ReadEnderecoDTO</returns>
        [HttpGet]
        public IEnumerable<ReadEnderecoDTO> RecuperaEnderecos()
        {
            return _mapper.Map<List<ReadEnderecoDTO>>(_context.Enderecos);
        }

        /// <summary>
        /// Consulta um endereço de acordo com id informado.
        /// </summary>
        /// <param name="id">Parâmetro obrigatório inteiro informando o id do endereço a ser consultado</param>
        /// <returns>IActionResult</returns>
        [HttpGet("{id}")]
        public IActionResult RecuperaEnderecosPorId(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco != null)
            {
                ReadEnderecoDTO enderecoDTO = _mapper.Map<ReadEnderecoDTO>(endereco);

                return Ok(enderecoDTO);
            }
            return NotFound();
        }

        /// <summary>
        /// Atualiza um endereço de acordo com id informado.
        /// </summary>
        /// <param name="id">Parâmetro obrigatório inteiro informando o id do endereço a ser atualizado</param>
        /// <param name="enderecoDTO">Parâmetro obrigatório informando o objeto completo para atualização de um endereço</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDTO enderecoDTO)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }
            _mapper.Map(enderecoDTO, endereco);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deleta um endereço.
        /// </summary>
        /// <param name="id">Parâmetro obrigatório inteiro informando o id do endereço a ser deletado</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public IActionResult DeletaEndereco(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }
            _context.Remove(endereco);
            _context.SaveChanges();
            return NoContent();
        }

    }
}