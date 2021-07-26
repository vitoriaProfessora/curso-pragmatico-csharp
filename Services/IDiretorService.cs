using System.Collections.Generic;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IDiretorService {
    Task<DiretorListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken);
    Task<Diretor> GetById(long id);
    Task<Diretor> Cria(Diretor diretor);
    Task<Diretor> Atualiza(Diretor diretor, long id);
    Task Exclui(long id);
}
