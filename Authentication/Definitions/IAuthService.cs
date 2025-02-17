using AuthenticationAPI.Models;

namespace AuthenticationAPI.Definitions
{
    public interface IAuthService
    {
        public Task<bool> IsValidUser(User user);
    }
}
