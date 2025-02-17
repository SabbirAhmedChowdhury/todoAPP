using AuthenticationAPI.Models;

namespace AuthenticationAPI.Definitions
{
    public interface IAuthRepository
    {
        public Task<User> GetUserByUsername(String? username);
    }
}
