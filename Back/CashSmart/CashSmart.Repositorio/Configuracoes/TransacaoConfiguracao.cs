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
            builder.Property(t => t.Valor).HasColumnName("Valor").IsRequired();
            builder.Property(t => t.DataCriacao).HasColumnName("DataCriacao").IsRequired();
            builder.Property(t => t.DataAtualizacao).HasColumnName("DataAtualizacao").IsRequired();


            builder.HasOne(t => t.FormaPagamento).WithMany(fp => fp.Transacoes).HasForeignKey(t => t.FormaPagamentoId);

            builder.HasOne(t => t.Categoria).WithMany(c => c.Transacoes).HasForeignKey(t => t.CategoriaId);

            builder.HasMany(t => t.Parcelas).WithOne(p => p.Transacao).HasForeignKey(p => p.TransacaoId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}