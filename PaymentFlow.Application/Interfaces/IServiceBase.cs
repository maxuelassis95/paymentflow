using System.Linq.Expressions;
using System.Net.Http.Headers;
using PaymentFlow.Domain.Commons;
using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Application.Interfaces
{
    public interface IServiceBase<TEntity> where TEntity : EntidadeBase
    {
        public Task<Result> AddAsync(TEntity entity, CancellationToken ct);
        public Task<Result<TEntity>> GetAsync(int id, CancellationToken ct);
        public Task<IReadOnlyList<Result<TEntity>>> GetaAlAsync(CancellationToken ct);
        public Task<IReadOnlyList<Result<TEntity>>> FindAsync(Expression<Func<TEntity, bool>> predicated, CancellationToken ct);
    }
}