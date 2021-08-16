using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IFilmeService {
    Task<FilmeListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken);
    Task<Filme> GetById(long id);
    Task<Filme> Cria(Filme filme);
    Task<Filme> Atualiza(Filme filme, long id);
    Task Exclui(long id);
}