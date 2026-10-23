using System.Linq.Expressions;
using System.Net.Http.Headers;
using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Application.Interfaces
{
    public interface IServiceBase<TEntity> where TEntity : EntidadeBase
    {
        public Task<bool> AddAsync(TEntity entity, CancellationToken ct);
        public Task<TEntity> GetAsync(int id, CancellationToken ct);
        public Task<IReadOnlyList<TEntity>> GetaAlAsync(CancellationToken ct);
        public Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicated, CancellationToken ct);
    }
}