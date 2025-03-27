using CashSmart.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashSmart.Repositorio.Configuracoes
{
    public class FormaPagamentoConfiguracao : IEntityTypeConfiguration<FormaPagamento>
    {
        public void Configure(EntityTypeBuilder<FormaPagamento> builder)
        {
            builder.HasKey(fp => fp.Id);

            builder.Property(fp => fp.Id).HasColumnName("Id");
            builder.Property(fp => fp.Nome).HasColumnName("Nome").HasMaxLength(127).IsRequired();
            builder.Property(fp => fp.DataCriacao).HasColumnName("DataCriacao").IsRequired();
            builder.Property(fp => fp.DataAtualizacao).HasColumnName("DataAtualizacao").IsRequired();      
            builder.Property(fp => fp.UsuarioId).HasColumnName("UsuarioId").IsRequired(); 

            builder.HasMany(fp => fp.Transacoes).WithOne(t => t.FormaPagamento).HasForeignKey(t => t.FormaPagamentoId).OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}