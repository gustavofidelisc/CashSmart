using Microsoft.EntityFrameworkCore;
using CashSmart.Repositorio.Configuracoes;
using CashSmart.Dominio.Entidades;

namespace CashSmart.Repositorio.Contexto;
public class CashSmartContexto : DbContext
{
    private readonly DbContextOptions<CashSmartContexto> _options;
    public DbSet<Usuario> Usuarios { get; set; }

    public CashSmartContexto()
    {
        
    }
    public CashSmartContexto(DbContextOptions<CashSmartContexto> options) : base(options)
    {
        _options = options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioConfiguracao());
    }

}
