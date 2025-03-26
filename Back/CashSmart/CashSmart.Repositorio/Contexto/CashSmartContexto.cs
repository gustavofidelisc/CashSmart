using Microsoft.EntityFrameworkCore;
using CashSmart.Repositorio.Configuracoes;
using CashSmart.Dominio.Entidades;

namespace CashSmart.Repositorio.Contexto;
public class CashSmartContexto : DbContext
{
    private readonly DbContextOptions<CashSmartContexto> _options;
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<FormaPagamento> FormasPagamento { get; set; }
    public DbSet<Parcela> Parcelas { get; set;}

    public CashSmartContexto()
    {
        
    }
    public CashSmartContexto(DbContextOptions<CashSmartContexto> options) : base(options)
    {
        _options = options;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-Q3A4OIP;Database=CashSmart;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioConfiguracao());
        modelBuilder.ApplyConfiguration(new CategoriaConfiguracao());
        modelBuilder.ApplyConfiguration(new FormaPagamentoConfiguracao());
        modelBuilder.ApplyConfiguration(new ParcelaConfiguracao());
    }

}
