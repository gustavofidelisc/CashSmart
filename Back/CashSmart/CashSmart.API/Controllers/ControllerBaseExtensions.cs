using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

public static class ControllerBaseExtensions
{
    
    public static Guid ObterUsuarioIdDoHeader(this ControllerBase controller)
    {
        
        var claimValue = controller.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(!Guid.TryParse(claimValue, out Guid userId))
        {
            throw new ArgumentException("Id do usuário inválido");
        }
        return userId;
        
    }
}