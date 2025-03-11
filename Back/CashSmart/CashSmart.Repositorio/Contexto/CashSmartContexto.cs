using Microsoft.EntityFrameworkCore;
using CashSmart.Repositorio.Configuracoes;
using CashSmart.Dominio.Entidades;

namespace CashSmart.Repositorio.Contexto;
public class CashSmartContexto : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=LAPTOP-C2QELPB5;Database=CashSmart;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioConfiguracao());
    }

}
