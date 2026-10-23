using PaymentFlow.Application.DTOs;
using PaymentFlow.Domain.Commons;
using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Application.Interfaces
{

    // A comunidade é bem clara em não usar genérics nas services IServiceBase<> e ServiceBase<> ..
    public interface IClienteService
    {
        public Task<Result> CreateAsync(ClienteRequestDTO clienteDTO, CancellationToken ct);
        public Task<Result<Cliente>> GetByIdAsync(int id, CancellationToken ct);
        public Task<Result<IReadOnlyList<Cliente>>> GetAll(CancellationToken ct);
        public Task<Result<IReadOnlyList<Cliente>>> FindByEmailAsync(string email, CancellationToken ct);
    }
}