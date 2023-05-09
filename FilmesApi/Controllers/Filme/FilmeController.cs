using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.DTOs;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao banco de dados.
    /// </summary>
    /// <param name="filmeDTO">Parâmetro obrigatório do objeto completo informando os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDTO filmeDTO)
    {
        Filme filme = _mapper.Map<Filme>(filmeDTO);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id }, filme);
    }

    /// <summary>
    /// Consulta os filmes cadastrados.
    /// </summary>
    /// <param name="skip">Parâmetro opcional inteiro informando a posição inicial para filtragem</param>
    /// <param name="take">Parâmetro opcional inteiro informando quantos filmes irá filtrar a partir do skip para filtragem</param>
    /// <param name="nomeCinema">Parâmetro opcional informando o nome do cinema para que apenas filmes deste cinema sejam consultados</param>
    /// <returns>IEnumerable de ReadFilmeDTO</returns>
    [HttpGet]
    public IEnumerable<ReadFilmeDTO> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50, [FromQuery] string? nomeCinema = null)
    {
        if(nomeCinema == null)
            return _mapper.Map<List<ReadFilmeDTO>>(_context.Filmes.Skip(skip).Take(take).ToList());
        return _mapper.Map<List<ReadFilmeDTO>>(_context.Filmes.Skip(skip).Take(take).Where(filme => filme.Sessoes.Any(sessao => sessao.Cinema.Nome == nomeCinema)).ToList());
    }

    /// <summary>
    /// Consulta um filme pelo Id informado.
    /// </summary>
    /// <param name="id">Parâmetro obrigatório inteiro informando o id do filme a ser consultado</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null)
            return NotFound();
        var filmeDTO = _mapper.Map<ReadFilmeDTO>(filme);
        return Ok(filme);
    }

    /// <summary>
    /// Atualiza um filme de acordo com id informado.
    /// </summary>
    /// <param name="id">Parâmetro obrigatório inteiro informando o id do filme a ser atualizado</param>
    /// <param name="filmeDTO">Parâmetro obrigatório informando o objeto completo para atualização de um filme</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDTO filmeDTO)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null)
            return NotFound();
        _mapper.Map(filmeDTO, filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Atualiza parcialmente um filme pelo Id informado e pelo seu patch de atualização, 
    /// ou seja, não é necessário informar o objeto completo.
    /// </summary>
    /// <param name="id">Parâmetro obrigatório inteiro informando o id do filme a ser atualizado</param>
    /// <param name="patch">Parâmetro obrigatório do tipo JsonPatchDocument de UpdateFilmeDTO informando o patch de campos atualizados</param>
    /// <returns>IActionResult</returns>
    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDTO> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null)
            return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDTO>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
            return ValidationProblem(ModelState);

        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deleta um filme.
    /// </summary>
    /// <param name="id">Parâmetro obrigatório inteiro informando o id do filme a ser deletado</param>
    /// <returns>IActionResult</returns>
    [HttpDelete("{id}")]
    public IActionResult DeletaFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null)
            return NotFound();
        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}