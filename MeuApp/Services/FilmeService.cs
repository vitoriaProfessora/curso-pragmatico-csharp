using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class FilmeService : IFilmeService {
    private readonly ApplicationDbContext _context;
    public FilmeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<FilmeListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken) {
        var pagedModel = await _context.Filmes
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .PaginateAsync(page, limit, cancellationToken);

        if (!pagedModel.Items.Any()) {
            throw new Exception("Não existem filmes cadastrados!");
        }

        return new FilmeListOutputGetAllDTO {
            CurrentPage = pagedModel.CurrentPage,
            TotalPages = pagedModel.TotalPages,
            TotalItems = pagedModel.TotalItems,
            Items = pagedModel.Items.Select(filme => new FilmeOutPutGetAllDTO(filme.Id, filme.Titulo)).ToList()
        };
    }

    public async Task<Filme> GetById(long id) {
        var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);

        if (filme == null) {
            throw new Exception("Filme não encontrado!");
        }

        return filme;
    }

    public async Task<Filme> Cria(Filme filme) {
        var diretor = _context.Diretores.FirstOrDefault(diretor => diretor.Id == filme.DiretorId);

        if (diretor == null) {
            throw new Exception("Diretor não encontrado!");
        }

        _context.Filmes.Add(filme);
        await _context.SaveChangesAsync();
        return filme;
    }

    public async Task<Filme> Atualiza(Filme filme, long id) {
        var diretor = _context.Diretores.FirstOrDefault(diretor => diretor.Id == filme.DiretorId);

        if (diretor == null) {
            throw new Exception("Diretor não encontrado!");
        }

        var filmeNoDb = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filmeNoDb == null) {
            throw new Exception("Filme não encontrado!");
        }

        filmeNoDb.Titulo = filme.Titulo;
        filmeNoDb.DiretorId = filme.DiretorId;

        _context.Filmes.Update(filmeNoDb);
        await _context.SaveChangesAsync();

        return filmeNoDb;
    }

    public async Task Exclui(long id) {
        var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);
        _context.Remove(filme);
        await _context.SaveChangesAsync();
    }
}