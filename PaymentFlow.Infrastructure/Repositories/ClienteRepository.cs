using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Interfaces;
using PaymentFlow.Infrastructure.Data;

namespace PaymentFlow.Infrastructure.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        private PaymentFlowContext _context;

        // Construto recebendo o PaymentFlowContext do DI e passando para a classe mãe que herdamos.
        public ClienteRepository(PaymentFlowContext context) : base (context)
        {
            _context = context;
        }
        
        //Por enquanto sem métodos, pois eles estão na RepositoryBase<>

    }
}