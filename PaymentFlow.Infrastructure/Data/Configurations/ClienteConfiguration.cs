using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Infrastructure.Data.Configurations
{
    // Classe responsavel por mapear e configurar as tabelas do banco de acordo com as entidades
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("clientes");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.DataCriacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .ValueGeneratedOnAdd();

            builder.HasMany(c => c.Pagamentos)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}