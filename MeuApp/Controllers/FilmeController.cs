using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase {
    private readonly IFilmeService _filmeService;

    public FilmeController(IFilmeService filmeService) {
        _filmeService = filmeService;
    }

    /// <summary>
    /// Busca todos os filmes do sistema
    /// </summary>
    /// <param name="nome">Nome do filme</param>
    /// <returns>Exibe os filmes</returns>
    /// <response code="200">Sucesso ao buscar todos os filmes</response>
    /// <response code="404">Não existe nenhum filme cadastrado</response>
    /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

    // GET api/filmes
    [HttpGet]
    public async Task<ActionResult<FilmeListOutputGetAllDTO>> Get(CancellationToken cancellationToken, int limit = 5, int page = 1) {
        return await _filmeService.GetByPageAsync(limit, page, cancellationToken);        
    }

    /// <summary>
    /// Busca um filme pelo seu Id
    /// </summary>
    /// <param name="nome">Id do filme</param>
    /// <returns>Exibe o filme buscado</returns>
    /// <response code="200">Sucesso ao retornar o filme</response>
    /// <response code="404">Não existe filme com esse Id informado</response>
    /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

    // GET api/filmes/1
    [HttpGet("{id}")]
    public async Task<ActionResult<FilmeOutputGetByIdDTO>> Get(long id) {
        var filme = await _filmeService.GetById(id);

        var outputDTO = new FilmeOutputGetByIdDTO(filme.Id, filme.Titulo, filme.Diretor.Nome);
        return Ok(outputDTO);
    }

    /// <summary>
    /// Cria um filme
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /filme
    ///     {
    ///        "titulo": "A Cabana",
    ///        "diretorId": 1
    ///     }
    ///
    /// </remarks>
    /// <param name="inputDto">Nome do filme</param>
    /// <returns>O Filme criado</returns>
    /// <response code="200">Filme foi criado com sucesso</response>
    /// <response code="400">Não foi possível cadastrar um filme</response>
    /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

    // POST api/filmes
    [HttpPost]
    public async Task<ActionResult<FilmeOutputPostDTO>> Post([FromBody] FilmeInputPostDTO inputDTO) {
        var filme = await _filmeService.Cria(new Filme(inputDTO.Titulo, inputDTO.DiretorId));

        var outputDTO = new FilmeOutputPostDTO(filme.Id, filme.Titulo);
        
        return Ok(outputDTO);
    }

    /// <summary>
    /// Atualiza um filme criado
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /filme
    ///     {
    ///        "titulo": "A Cabana",
    ///        "diretorId": 1
    ///     }
    ///
    /// </remarks>
    /// <param name="filmeId">Id do filme</param>
    /// <param name="nome">Nome do filme</param>
    /// <returns>O filme foi atualizado com sucesso</returns>
    /// <response code="200">O filme foi atualizado com sucesso</response>
    /// <response code="400">Não foi possível atualizar o filme com o Id informado</response>
    /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

    // PUT api/filmes/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<FilmeOutputPutDTO>> Put(long id, [FromBody] FilmeInputPutDTO inputDTO) {
        var filme = new Filme(inputDTO.Titulo, inputDTO.DiretorId);

        var filmeModificado = await _filmeService.Atualiza(filme, id);

        var outputDTO = new FilmeOutputPutDTO(filmeModificado.Id, filmeModificado.Titulo);
        return Ok(outputDTO);
    }

    /// <summary>
    /// Exclui um filme
    /// </summary>
    /// <param name="filmeId">Id do filme</param>
    /// <returns>Filme excluído</returns>
    /// <response code="200">O filme foi excluído com sucesso</response>
    /// <response code="404">Não foi possível excluir o filme com o Id informado</response>
    /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

    // DELETE api/filmes/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id) {
        await _filmeService.Exclui(id);
        return Ok();
    }
}