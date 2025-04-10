namespace CashSmart.Aplicacao.Services.Criptografia.Interface
{
    public interface IBcryptSenhaService
    {
        string CriptografarSenha(string senha);
        bool VerificarSenha(string senha, string hashSenha);
    }
}