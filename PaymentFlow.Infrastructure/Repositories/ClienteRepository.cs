using System.Data.Common;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Interfaces;
using PaymentFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

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


        // Sobrescreve o AddAsync da RepositoryBase<> porque o cadastro de Cliente exige validação
        // de unicidade de e-mail, evitando duplicidade no banco.
        //
        // A validação é feita em duas camadas:
        //
        // 1) Verificação prévia (FindAsync): cobre a maioria dos casos e devolve um retorno
        //    amigável rapidamente. Porém, sozinha, NÃO é suficiente: existe condição de corrida
        //    (duplo submit, retry de cliente HTTP, requisições simultâneas). Duas requisições
        //    podem passar pela verificação ao mesmo tempo e tentar inserir o mesmo e-mail.
        //
        // 2) Índice único no banco: garantia FINAL de unicidade. Definido na Configuration:
        //        builder.HasIndex(c => c.Email).IsUnique();
        //    Sem esse índice, a verificação prévia é apenas "boa vontade" da aplicação.
        //
        // Quando o índice único é violado, o SaveChangesAsync lança DbUpdateException. Com
        // Pomelo (MySQL), a exceção interna é MySqlException com Number = 1062 (Duplicate entry).
        // O catch abaixo captura esse caso residual e devolve o mesmo retorno amigável da
        // verificação prévia, sem vazar exceção para o usuário.
        public async Task<bool> AddAsync(Cliente cliente, CancellationToken ct = default)
        {
            var conta = await FindAsync(c => c.Email == cliente.Email, ct);
            if (conta.Count > 0)
            {
                return false;
            }

            await _context.Clientes.AddAsync(cliente, ct);

            try
            {
                await _context.SaveChangesAsync(ct);
                return true;
            }
            catch (DbUpdateException ex) when (ex.InnerException is MySqlException { Number: 1062 })
            {
                _context.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                return false;
            }
        }

        public async Task<Cliente> GetByEmailAsync(string email, CancellationToken ct)
        {
            return await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email == email, ct);
        }

    }
}