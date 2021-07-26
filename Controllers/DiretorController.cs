using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class DiretorController : ControllerBase {
    private readonly ApplicationDbContext _context;
    private readonly IDiretorService _diretorService;

    public DiretorController(ApplicationDbContext context, IDiretorService diretorService) {
        _context = context;
        _diretorService = diretorService;
    }

    // GET api/diretores
    [HttpGet]
    public async Task<ActionResult<DiretorListOutputGetAllDTO>> Get(CancellationToken cancellationToken, int limit = 5, int page = 1) {
        return await _diretorService.GetByPageAsync(limit, page, cancellationToken);
    }

    // GET api/diretores/1
    [HttpGet("{id}")]
    public async Task<ActionResult<DiretorOutputGetByIdDTO>> Get(long id) {
        var diretor = await _diretorService.GetById(id);

        var outputDto = new DiretorOutputGetByIdDTO(diretor.Id, diretor.Nome);
        return Ok(outputDto);
    }

    /// <summary>
    /// Cria um diretor
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /diretor
    ///     {
    ///        "nome": "Martin Scorsese"
    ///     }
    ///
    /// </remarks>
    /// <param name="diretorInputDto">Nome do diretor</param>
    /// <returns>O diretor criado</returns>
    /// <response code="200">Diretor foi criado com sucesso</response>
    [HttpPost]
    public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputDto) {
        var diretor = await _diretorService.Cria(new Diretor(diretorInputDto.Nome));

        var diretorOutputDto = new DiretorOutputPostDTO(diretor.Id, diretor.Nome);
        return Ok(diretorOutputDto);
    }

    // PUT api/diretores/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<DiretorOuputPutDTO>> Put(long id, [FromBody] DiretorInputPutDTO diretorInputDto) {
        var diretor = await _diretorService.Atualiza(new Diretor(diretorInputDto.Nome), id);

        var diretorOutputDto = new DiretorOuputPutDTO(diretor.Id, diretor.Nome);
        return Ok(diretorOutputDto);
    }

    // DELETE api/diretores/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id) {
        await _diretorService.Exclui(id);
        return Ok();
    }
}