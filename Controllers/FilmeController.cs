using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase {
    private readonly ApplicationDbContext _context;

    public FilmeController(ApplicationDbContext context) {
        _context = context;
    }

    /// <summary>
    /// Busca todos os filmes
    /// </summary>
    /// <returns>Lista de filmes criados </returns>
    /// <response code="200">Sucesso ao buscar os filmes</response>
    /// <response code="404">Não existe nenhum filme cadastrado</response>
    /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

    // GET api/filmes
    [HttpGet]
    public async Task<ActionResult<List<FilmeOutPutGetAllDTO>>> Get() {
        var filmes = await _context.Filmes.ToListAsync();

        if (!filmes.Any()) {
            return NotFound("Não existe filmes cadastrados");
        }

        var outputDTOList = new List<FilmeOutPutGetAllDTO>();

        foreach (Filme filme in filmes) {
            outputDTOList.Add(new FilmeOutPutGetAllDTO(filme.Id, filme.Titulo));
        }

        return outputDTOList;
    }

    /// <summary>
    /// Busca um filme pelo Id
    /// </summary>
    /// <returns>Sucesso ao buscar o filme pelo Id Informado</returns>
    /// <param name="titulo">Titulo do filme</param>
    /// <response code="200">Busca um filme pelo Id informado</response>
    /// <response code="404">Não foi possível buscar o filme pelo Id informado</response>
    /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

    // GET api/filmes/1
    [HttpGet("{id}")]
    public async Task<ActionResult<FilmeOutputGetByIdDTO>> Get(long id) {
        var filme = await _context.Filmes.Include(filme => filme.Diretor).FirstOrDefaultAsync(filme => filme.Id == id);

        var outputDTO = new FilmeOutputGetByIdDTO(filme.Id, filme.Titulo, filme.Diretor.Nome);
        return Ok(outputDTO);
    }

    /// <summary>
    /// Cadastra um filme
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /filme
    ///     {
    ///        "titulo": "A Cabana",
    ///        "ano": "2016",
    ///        "genero": "Drama",
    ///        "diretorId": 1
    ///     }
    ///
    /// </remarks>
    /// <param name="nome">Titulo do filme</param>
    /// <param name="ano">Ano do filme</param>
    /// <param name="genero">Gênero do filme</param>
    /// <param name="diretor">Diretor do filme</param>
    /// <returns>O filme foi cadastrado com sucesso</returns>
    /// <response code="200">O filme foi cadastrado com sucesso</response>
    /// <response code="400">Não foi possível cadastrar o filme</response>
    /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

    // POST api/filmes
    [HttpPost]
    public async Task<ActionResult<FilmeOutputPostDTO>> Post([FromBody] FilmeInputPostDTO inputDTO) {
        var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == inputDTO.DiretorId);

        if (diretor == null) {
            return NotFound("Diretor informado não encontrado!");
        }

        var filme = new Filme(inputDTO.Titulo, diretor.Id);
        _context.Filmes.Add(filme);
        await _context.SaveChangesAsync();

        var outputDTO = new FilmeOutputPostDTO(filme.Id, filme.Titulo);
        
        
        return Ok(outputDTO);
    }

    /// <summary>
    /// Atualiza um filme
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /filme
    ///     {
    ///        "titulo": "A Cabana",
    ///        "ano": "2019",
    ///        "genero": "Drama",
    ///     }
    /// </remarks>
    /// <param name="nome">Titulo do filme</param>
    /// <param name="ano">Ano do filme</param>
    /// <param name="genero">Gênero do filme</param>
    /// <returns>O filme foi atualizado com sucesso</returns>
    /// <response code="200">O filme foi atualizado com sucesso</response>
    /// <response code="400">Não foi possível atualizar o filme com o Id informado</response>
    /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

    // PUT api/filmes/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<FilmeOutputPutDTO>> Put(long id, [FromBody] FilmeInputPutDTO inputDTO) {
        var filme = new Filme(inputDTO.Titulo, inputDTO.DiretorId);

        if (inputDTO.DiretorId == 0) {
            return NotFound("Id do diretor é inválido");
        }

        filme.Id = id;
        _context.Filmes.Update(filme);
        await _context.SaveChangesAsync();

        var outputDTO = new FilmeOutputPutDTO(filme.Id, filme.Titulo);
        return Ok(outputDTO);
    }

    /// <summary>
    /// Exclui um filme
    /// </summary>
    /// <returns>Exclui um filme</returns>
    /// <param name="filmeId">Id do filme</param>
    /// <response code="200">O filme foi excluido com sucesso</response>
    /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

    // DELETE api/filmes/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id) {
        var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);
        _context.Remove(filme);
        await _context.SaveChangesAsync();
        return Ok(filme);
    }
}