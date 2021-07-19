using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

[ApiController]
[Route("[controller]")]
public class DiretorController : ControllerBase {
    private readonly ApplicationDbContext _context;

    private readonly IDiretorService _diretorService;

    public DiretorController(ApplicationDbContext context,
                                DiretorService diretorService) {
        _context = context;
        _diretorService = diretorService;
    }

    /// <summary>
    /// Busca todos os diretores do sistema
    /// </summary>
    /// <param name="nome">Nome do diretor</param>
    /// <returns>Exibe os diretores</returns>
    /// <response code="200">Sucesso ao buscar todos os diretores</response>
    /// <response code="404">Não existe nenhum diretor cadastrado</response>
    /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

    // GET api/diretores
    [HttpGet]
    public async Task<ActionResult<List<DiretorOutputGetAllDTO>>> Get() {
        var diretores = await _diretorService.GetAll();

        var outputDtoList = new List<DiretorOutputGetAllDTO>();

        foreach (Diretor diretor in diretores) {
            outputDtoList.Add(new DiretorOutputGetAllDTO(diretor.Id, diretor.Nome));
        }

        return outputDtoList;
    }

    /// <summary>
    /// Busca um diretor pelo seu Id
    /// </summary>
    /// <param name="nome">Id do diretor</param>
    /// <returns>Exibe o diretor buscado</returns>
    /// <response code="200">Sucesso ao retornar o diretor</response>
    /// <response code="404">Não existe diretor com esse Id informado</response>
    /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
    // GET api/diretores/1

    [HttpGet("{id}")]
    public async Task<ActionResult<DiretorOutputGetByIdDTO>> Get(long id) {
        var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);

        if (diretor == null) {
            return NotFound("Diretor não encontrado");
        }
        
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
    ///        "nome": "José Pires",
    ///     }
    ///
    /// </remarks>
    /// <param name="nome">Nome do diretor</param>
    /// <returns>O diretor foi criado com sucesso</returns>
    /// <response code="200">O diretor foi criado com sucesso</response>
    /// <response code="400">Não foi possível cadastrar um diretor</response>
    /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

    // POST api/diretores
    [HttpPost]
    public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputDto) {
        var diretor = new Diretor(diretorInputDto.Nome);
        _context.Diretores.Add(diretor);                    
        
        await _context.SaveChangesAsync();

        var diretorOutputDto = new DiretorOutputPostDTO(diretor.Id, diretor.Nome);
        return Ok(diretorOutputDto);
    }

     /// <summary>
    /// Atualiza um diretor criado
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /diretor
    ///     {
    ///        "nome": "José Souza",
    ///     }
    ///
    /// </remarks>
    /// <param name="diretorId">Id do diretor</param>
    /// <param name="nome">Nome do diretor</param>
    /// <returns>O diretor foi atualizado com sucesso</returns>
    /// <response code="200">O diretor foi atualizado com sucesso</response>
    /// <response code="400">Não foi possível atualizar o diretor com o Id informado</response>
    /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

    // PUT api/diretores/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<DiretorOuputPutDTO>> Put(long id, [FromBody] DiretorInputPutDTO diretorInputDto) {
        var diretor = new Diretor(diretorInputDto.Nome);
        diretor.Id = id;
        _context.Diretores.Update(diretor);
        await _context.SaveChangesAsync();

        var diretorOutputDto = new DiretorOuputPutDTO(diretor.Id, diretor.Nome);
        return Ok(diretorOutputDto);
    }

    /// <summary>
    /// Exclui um diretor
    /// </summary>
    /// <param name="diretorId">Id do diretor</param>
    /// <returns>Diretor excluidoO diretor foi excluido com sucesso</returns>
    /// <response code="200">O diretor foi excluido com sucesso</response>
    /// <response code="404">Não foi possível excluir o diretor com o Id informado</response>
    /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

    // DELETE api/diretores/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id) {
        var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
        _context.Remove(diretor);
        await _context.SaveChangesAsync();
        return Ok();
    }
}