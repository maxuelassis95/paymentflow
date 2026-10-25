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

            builder.Property(c => c.Email)
                .IsRequired();

            // Diferença entre Property() e HasIndex():
            //
            // - builder.Property(c => c.Email) configura a COLUNA em si: tipo, tamanho máximo,
            //   obrigatoriedade (IsRequired), valor padrão, precisão, etc. É "como o dado é
            //   armazenado". Property() NÃO garante unicidade — não existe .Unique() nele.
            //
            // - builder.HasIndex(c => c.Email) configura um ÍNDICE sobre a coluna, que é uma
            //   estrutura separada no banco. Combinado com .IsUnique(), é o que garante a
            //   unicidade do e-mail no nível do banco de dados.
            //
            // Analogia: Property() define "como o campo é" (texto obrigatório de até N caracteres);
            // HasIndex() cria uma "etiqueta organizada" por cima desse campo, usada para buscar
            // mais rápido ou para impedir valores repetidos.
            //
            // Por isso a unicidade do e-mail é definida aqui, e não no Property().
            builder.HasIndex(c => c.Email)
                   .IsUnique();

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