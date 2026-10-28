using PaymentFlow.Domain.Interfaces;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PaymentFlow.Infrastructure.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntidadeBase
    {
        // campo responsavel por guardar a instancia do PaymentFlowContext que vamos receber pelo construtor fornecido pelo DI
        protected PaymentFlowContext _context;

        public RepositoryBase(PaymentFlowContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(TEntity entity, CancellationToken ct = default)
        {
            await _context.Set<TEntity>() // por ser uma classe genérica, usamos o .Set<> para informar qual tabela vai ser usada, passando nele o TEntity 
                .AddAsync(entity, ct);

            var rows = await _context.SaveChangesAsync(ct); // momento que ocorre de fato a alteração no banco
            
            return rows > 0;
        }

        // AsNoTracking() deve ser usado para retorno de leitura, ele diz ao EF que não precisa deixar nada em Change Tracker
        public async Task<TEntity> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(entity => entity.Id == id, ct);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default) // Lembrar de inicialziar o CancellationToken com um valor default
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync(ct);
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