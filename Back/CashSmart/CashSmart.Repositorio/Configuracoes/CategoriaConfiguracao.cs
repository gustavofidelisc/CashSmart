using CashSmart.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashSmart.Repositorio.Configuracoes
{
    public class CategoriaConfiguracao : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.Nome).HasColumnName("Nome").HasMaxLength(127).IsRequired();
            builder.Property(c => c.DataCriacao).HasColumnName("DataCriacao").IsRequired();
            builder.Property(c => c.DataAtualizacao).HasColumnName("DataAtualizacao").IsRequired();       
            
        }
    }
}