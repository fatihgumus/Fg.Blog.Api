using Fg.Blog.Api.ViewModels;

namespace Fg.Blog.Api.Services
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string actualPassword, string hashedPassword);
        AuthData GetAuthData(long id);
    }
}