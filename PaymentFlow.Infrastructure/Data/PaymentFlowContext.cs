using Microsoft.EntityFrameworkCore;
using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Infrastructure.Data
{
    // Só conseguimos herdad DbContext porque adicionamos a biliotece Microsoft.EntityFrameworkCore
    // No caso adicionamos a biblioteca Pomelo.EntityFrameworkCore.MySql, mas como ela precisa Microsoft.EntityFrameworkCore, 
    //                  ela ja traz ela.
    public class PaymentFlowContext : DbContext
    {

        // O construtor de PaymentFlowContext recebe um parâmetro chamado options, cujo tipo é DbContextOptions específico para PaymentFlowContext
        // E repassa esse options para o construtor da classe base DbContext
        public PaymentFlowContext(DbContextOptions<PaymentFlowContext> options) : base (options)
        {

        }

        // propriedades que representam as tabelas do banco
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }

    }
}