using System.Security.Claims;

namespace Services.B.Core.Interfaces
{
    public interface IJwtFactory
    {
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
