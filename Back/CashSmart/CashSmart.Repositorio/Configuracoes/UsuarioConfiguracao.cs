using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CashSmart.Dominio.Entidades;
namespace CashSmart.Repositorio.Configuracoes
{
    class UsuarioConfiguracao : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("Id").HasDefaultValueSql("NEWID()").IsRequired();
            builder.Property(u => u.Nome).HasColumnName("Nome").HasMaxLength(127).IsRequired();
            builder.Property(u => u.Email).HasColumnName("Email").HasMaxLength(127).IsRequired();
            builder.Property(u => u.Senha).HasColumnName("Senha").HasMaxLength(127).IsRequired();
            builder.Property(u => u.Ativo).HasColumnName("Ativo").IsRequired();
            builder.Property(u => u.DataCriacao).HasColumnName("DataCriacao").IsRequired();
            builder.Property(u => u.DataAtualizacao).HasColumnName("DataAtualizacao").IsRequired();

            builder.HasMany(u => u.Transacoes).WithOne(t => t.Usuario).HasForeignKey(t => t.UsuarioId);
        }
    }
}