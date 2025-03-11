using CashSmart.Repositorio.Contexto;
public abstract class BaseRepositorio
{
    protected readonly CashSmartContexto _context;

    protected BaseRepositorio(CashSmartContexto context)
    {
        _context = context;
    }
}