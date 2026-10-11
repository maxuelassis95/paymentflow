using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;
using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Infrastructure.Data
{
    public class PaymentFlowContext : DbContext
    {

        // Construtor que ao ser instanciado vai receber como parametro DbContextOptions do tipo PaymentFlowContext
        public PaymentFlowContext(DbContextOptions<PaymentFlowContext> options) : base (options)
        {

        }

        // Propriedades que representam as tabelas
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }

        // Métodos responsavel por construir (build) as tabelas de acordo com as entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// não constrói as tabela, ele configura o mapeamente entre as tabelas e as entidades - as migrations cria as tabelas
            //modelBuilder.Entity<Cliente>(entity =>
            //{
            //    entity.ToTable("clientes");
            //    // indica a chave primaria através de uma expressão lambda
            //    entity.HasKey(c => c.Id);
            //    // configura o campo Nome com as regras obrigatório com maximoa de 250 caracteres, lembrando que aqui não cria nada,
            //    // apenas mapeia e configura as regras
            //    entity.Property(c => c.Nome).IsRequired().HasMaxLength(255);


            //    // 1 Cliente => N Pagamentos

            //    //HasMany: um cliente pode ter vários pagamentos.
            //    //WithOne: cada pagamento está associado a um cliente.
            //    //HasForeignKey: ClienteId é a chave estrangeira.
            //    //OnDelete(DeleteBehavior.Cascade): ao excluir um cliente, seus pagamentos relacionados também poderão ser excluídos.
            //    entity.HasMany(c => c.Pagamentos)
            //          .WithOne(p => p.Cliente)
            //          .HasForeignKey(p => p.ClienteId)
            //          .OnDelete(DeleteBehavior.Cascade);
            //});

            //modelBuilder.Entity<Pagamento>(entity => {
            //    entity.ToTable("pagamentos");
            //    entity.HasKey(p => p.Id);
            //    entity.Property(p => p.ClienteId).IsRequired();
            //    entity.Property(p => p.Valor).HasColumnType("decimal(10,2)").IsRequired();
            //});
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentFlowContext).Assembly);
        }

    }
}