using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Domain.Interfaces
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        Task<Cliente> GetByEmailAsync(string email, CancellationToken ct);
    }
}