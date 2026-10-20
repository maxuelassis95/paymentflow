using System.Linq.Expressions;
using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : EntidadeBase
    {
        public Task AddAsync(TEntity entidade);
        public Task<TEntity> GetAsync(int id, CancellationToken ct);
        public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct);

        // como quero apenas os dados é recomendado não usar o List<TEntity> e sim IReadOnlyList ou IReadOnlyCollection;
        // Lendo o método: é um método chamado Searchasync que é async e vai retornar uma Task com lista apenas de leitura,
        // ele recebe como parametro uma Expression<Func<TEntity>> que o Expression traduz em SQL a func e evita retornar toda a lista em memória;
        // e também recebe um CancellationToken ct que é passado no .ToListAsync() e diz "se o usuario deixar a sessão, cancele a busca".
        // AsNoTracking() deve ser usado para retorno de leitura, ele diz ao EF que não precisa deixar nada em Change Tracker
        public Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicated, CancellationToken ct);
    }
}