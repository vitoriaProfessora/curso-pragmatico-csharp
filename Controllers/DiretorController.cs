using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class DiretorController : ControllerBase {
    private readonly ApplicationDbContext _context;

    public DiretorController(ApplicationDbContext context) {
        _context = context;
    }

    // GET api/diretores
    [HttpGet]
    public async Task<List<DiretorOutputGetAllDTO>> Get() {
        var diretores = await _context.Diretores.ToListAsync();

        var outputDtoList = new List<DiretorOutputGetAllDTO>();

        foreach (Diretor diretor in diretores) {
            outputDtoList.Add(new DiretorOutputGetAllDTO(diretor.Id, diretor.Nome));
        }
        return outputDtoList;
    }

    // GET api/diretores/1
    [HttpGet("{id}")]
    public async Task<ActionResult<DiretorOutputGetByIdDTO>> Get(long id) {
        var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
        
        var outputDto = new DiretorOutputGetByIdDTO(diretor.Id, diretor.Nome);
        return Ok(outputDto);
    }

    // POST api/diretores
    [HttpPost]
    public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputDto) {
        var diretor = new Diretor(diretorInputDto.Nome);
        _context.Diretores.Add(diretor);                    
        
        await _context.SaveChangesAsync();

        var diretorOutputDto = new DiretorOutputPostDTO(diretor.Id, diretor.Nome);
        return Ok(diretorOutputDto);
    }

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

    // DELETE api/diretores/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id) {
        var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
        _context.Remove(diretor);
        await _context.SaveChangesAsync();
        return Ok();
    }
}