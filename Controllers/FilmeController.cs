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

    // GET api/filmes
    [HttpGet]
    public async Task<ActionResult<FilmeListOutputGetAllDTO>> Get(CancellationToken cancellationToken, int limit = 5, int page = 1) {
        return await _filmeService.GetByPageAsync(limit, page, cancellationToken);        
    }

    // GET api/filmes/1
    [HttpGet("{id}")]
    public async Task<ActionResult<FilmeOutputGetByIdDTO>> Get(long id) {
        var filme = await _filmeService.GetById(id);

        var outputDTO = new FilmeOutputGetByIdDTO(filme.Id, filme.Titulo, filme.Diretor.Nome);
        return Ok(outputDTO);
    }

    // POST api/filmes
    [HttpPost]
    public async Task<ActionResult<FilmeOutputPostDTO>> Post([FromBody] FilmeInputPostDTO inputDTO) {
        var filme = await _filmeService.Cria(new Filme(inputDTO.Titulo, inputDTO.DiretorId));

        var outputDTO = new FilmeOutputPostDTO(filme.Id, filme.Titulo);
        
        return Ok(outputDTO);
    }

    // PUT api/filmes/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<FilmeOutputPutDTO>> Put(long id, [FromBody] FilmeInputPutDTO inputDTO) {
        var filme = new Filme(inputDTO.Titulo, inputDTO.DiretorId);

        await _filmeService.Atualiza(filme, filme.Id);

        var outputDTO = new FilmeOutputPutDTO(filme.Id, filme.Titulo);
        return Ok(outputDTO);
    }

    // DELETE api/filmes/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id) {
        await _filmeService.Exclui(id);
        return Ok();
    }
}