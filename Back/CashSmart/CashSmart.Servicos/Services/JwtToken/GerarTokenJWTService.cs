using CashSmart.Repositorio.Contratos;
using CashSmart.Servicos.Services.JwtToken.Interface;

namespace CashSmart.Servicos.Services.JwtToken
{
    public class GerarTokenJWTService : IGerarTokenJWTService
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public GerarTokenJWTService(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;          
        }

        public string GerarTokenJWT()
        {
            throw new NotImplementedException();
        }
    }
}