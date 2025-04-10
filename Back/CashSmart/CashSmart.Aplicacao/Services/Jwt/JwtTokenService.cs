using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CashSmart.Dominio.Entidades;
using CashSmart.Dominio.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CashSmart.Aplicacao.Services.Jwt
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtConfiguracoes _jwtConfiguracoes;
        
        public JwtTokenService(IOptions<JwtConfiguracoes> jwtConfiguracoes)
        {
            _jwtConfiguracoes = jwtConfiguracoes.Value;
        }

        public string GerarTokenJWT(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfiguracoes.Key);
            var now = DateTime.UtcNow;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Configurações essenciais
                Issuer = _jwtConfiguracoes.Issuer,
                Audience = _jwtConfiguracoes.Audience,
                
                // Configurações de tempo
                NotBefore = now.AddMinutes(-1), // 1 minuto de margem
                
                Expires = now.AddMinutes(_jwtConfiguracoes.ExpirationInMinutes),

                // Dados do usuário
                Subject = GerarClaims(usuario),
                // Credenciais de assinatura
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static ClaimsIdentity GerarClaims(Usuario usuario){
            return new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
            ]);
        }
    }
}