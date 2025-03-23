using CashSmart.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashSmart.Repositorio.Configuracoes
{
    
    public class TransacaoConfiguracao : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("Id").IsRequired();
            builder.Property(t => t.Valor).HasColumnName("Valor").IsRequired();
            builder.Property(t => t.Ativo).HasColumnName("Ativo").IsRequired();
            builder.Property(t => t.DataCriacao).HasColumnName("DataCriacao").IsRequired();
            builder.Property(t => t.DataAtualizacao).HasColumnName("DataAtualizacao").IsRequired();
        }
    }
}