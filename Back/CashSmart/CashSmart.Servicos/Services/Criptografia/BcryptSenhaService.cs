using CashSmart.Servicos.Services.Criptografia.Interface;
using BCrypt.Net;
namespace CashSmart.Servicos.Services.Criptografia
{
    public class BcryptSenhaService : IBcryptSenhaService
    {
        public string CriptografarSenha(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool VerificarSenha(string senha, string hashSenha)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hashSenha);
        }
    }
}
