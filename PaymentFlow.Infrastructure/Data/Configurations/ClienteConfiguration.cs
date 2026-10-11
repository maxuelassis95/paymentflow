using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Infrastructure.Data.Configurations
{

    // Classe responsavel por separar as configurações da entidade do PaymentFlowContex, respeitando o conceito de responsabilidade unica
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("clientes");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome).IsRequired().HasMaxLength(255);

            builder.HasMany(c => c.Pagamentos)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}