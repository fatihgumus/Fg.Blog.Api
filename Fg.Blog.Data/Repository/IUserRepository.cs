

using Fg.Blog.Model;

namespace Fg.Blog.Data.Repository
{
    public interface IUserRepository : IEntityBaseRepository<User>
    {
        bool IsUsernameUniq(string username);
        bool isEmailUniq(string email);
    }
}