using Microsoft.EntityFrameworkCore;
using CashSmart.Dominio.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CashSmart.Dominio.Entidades
{
    public class ParcelaConfiguracao : IEntityTypeConfiguration<Parcela>
    {
        public void Configure(EntityTypeBuilder<Parcela> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.DataAtualizacao).HasColumnName("DataAtualizacao").IsRequired();
            builder.Property(p => p.DataVencimento).HasColumnName("DataVencimento").IsRequired();
            builder.Property(p => p.Valor).HasColumnName("Valor").IsRequired();
            builder.Property(p => p.NumeroDaParcela).HasColumnName("NumeroDaParcela").IsRequired();
            builder.Property(p => p.TransacaoID).HasColumnName("TransacaoID").IsRequired();
        }
    }
}