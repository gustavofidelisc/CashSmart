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
            builder.Property(t => t.Descricao).HasColumnName("Descricao").HasMaxLength(127).IsRequired();
            builder.Property(t => t.Data).HasColumnName("Data").IsRequired();
            builder.Property(t => t.Valor).HasColumnName("Valor").HasPrecision(2).IsRequired();
            builder.Property(t => t.DataCriacao).HasColumnName("DataCriacao").IsRequired();
            builder.Property(t => t.DataAtualizacao).HasColumnName("DataAtualizacao").IsRequired();
            builder.Property(t => t.UsuarioId).HasColumnName("UsuarioId").IsRequired();
            builder.Property(t => t.FormaPagamentoId).HasColumnName("FormaPagamentoId").IsRequired();
            builder.Property(t => t.CategoriaId).HasColumnName("CategoriaId").IsRequired();



            builder.HasMany(t => t.Parcelas).WithOne(p => p.Transacao).HasForeignKey(p => p.TransacaoId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}