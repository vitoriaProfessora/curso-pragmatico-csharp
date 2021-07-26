using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class DiretorService : IDiretorService {
    private readonly ApplicationDbContext _context;
    public DiretorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DiretorListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken) {
        var pagedModel = await _context.Diretores
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .PaginateAsync(page, limit, cancellationToken);

        if (!pagedModel.Items.Any()) {
            throw new Exception("Não existem diretores cadastrados!");
        }

        return new DiretorListOutputGetAllDTO {
            CurrentPage = pagedModel.CurrentPage,
            TotalPages = pagedModel.TotalPages,
            TotalItems = pagedModel.TotalItems,
            Items = pagedModel.Items.Select(diretor => new DiretorOutputGetAllDTO(diretor.Id, diretor.Nome)).ToList()
        };
    }

    public async Task<Diretor> GetById(long id) {
        var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);

        if (diretor == null) {
            throw new Exception("Diretor não encontrado!");
        }

        return diretor;
    }

    public async Task<Diretor> Cria(Diretor diretor) {
        _context.Diretores.Add(diretor);                    
        
        await _context.SaveChangesAsync();

        return diretor;
    }

    public async Task<Diretor> Atualiza(Diretor diretor, long id) {
        diretor.Id = id;
        _context.Diretores.Update(diretor);
        await _context.SaveChangesAsync();

        return diretor;
    }

    public async Task Exclui(long id) {
        var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
        _context.Remove(diretor);
        await _context.SaveChangesAsync();
    }
}
