using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Infrastructure.Data.Configurations
{
    public class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("pagamentos");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Cliente).IsRequired();
            builder.Property(p => p.Valor).HasColumnType("decimal(10:2)").IsRequired();
        }
    }
}