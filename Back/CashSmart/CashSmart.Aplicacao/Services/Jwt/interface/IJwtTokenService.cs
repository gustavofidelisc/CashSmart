using CashSmart.Dominio.Entidades;

namespace CashSmart.Aplicacao.Services.Jwt
{
    public interface IJwtTokenService
    {
        string GerarTokenJWT(Usuario usuario);
    }
}
