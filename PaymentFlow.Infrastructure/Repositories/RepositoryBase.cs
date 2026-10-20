using PaymentFlow.Domain.Interfaces;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Infrastructure.Data;

namespace PaymentFlow.Infrastructure.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntidadeBase
    {
        // campo que vai guardar a instancua do PaymentFlowContex qu vamos receber pelo construtor (passadas pelo Dependency Injection)
        protected PaymentFlowContext _context;
       
        // necessário construtor para receber as informações do PaymentFlowContext registradas pelo DI
        public RepositoryBase(PaymentFlowContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            // como nossa classe e método são genéricos precisamos saber o entitade e pra isso usamos Set<TEntity>()
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}