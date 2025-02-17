using AuthenticationAPI.Models;

namespace AuthenticationAPI.Definitions
{
    public interface ITokenService
    {
        string GenerateToken();
    }
}
