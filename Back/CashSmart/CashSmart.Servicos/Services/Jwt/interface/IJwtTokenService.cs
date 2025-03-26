using CashSmart.Dominio.Entidades;

namespace CashSmart.Servicos.Services.Jwt
{
    public interface IJwtTokenService
    {
        string GerarTokenJWT(Usuario usuario);
    }
}
