using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Interfaces;
using PaymentFlow.Infrastructure.Data;

namespace PaymentFlow.Infrastructure.Repositories
{
    public class PagamentoRepository : RepositoryBase<Pagamento>, IPagamentoRepository
    {
        private PaymentFlowContext _context;
        public PagamentoRepository(PaymentFlowContext context) : base (context)
        {
            _context = context;
        }

    }
}