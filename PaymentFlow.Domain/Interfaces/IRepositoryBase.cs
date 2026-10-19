using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : EntidadeBase
    {
        public Task AddAsync(TEntity entidade);
        public Task<TEntity> GetAsnyc(int id);
        public Task<List<TEntity>> GetAllAsync();
        public Task<List<TEntity>> SearchAsync(Func<TEntity, bool> predicated);
    }
}