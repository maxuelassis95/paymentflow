using PaymentFlow.Domain.Interfaces;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<TEntity> GetAsync(int id, CancellationToken ct = default)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, ct);            
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Set<TEntity>().ToListAsync(ct);
        }
        
        public async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicated, CancellationToken ct = default)
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .Where(predicated)
                .ToListAsync(ct);
        }
    }
}