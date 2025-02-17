using AuthenticationAPI.Definitions;
using AuthenticationAPI.Models;

namespace AuthenticationAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<bool> IsValidUser(User user)
        {
            //string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var userInfo = await _authRepository.GetUserByUsername(user.Username);

            if (userInfo.Username == user.Username && BCrypt.Net.BCrypt.Verify(user.Password, userInfo.Password))
                return await Task.FromResult(true);
            else
                return await Task.FromResult(false);
        }
    }
}
