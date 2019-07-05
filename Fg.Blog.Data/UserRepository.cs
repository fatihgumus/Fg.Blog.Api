

using Fg.Blog.Data.Repository;
using Fg.Blog.Model;

namespace Fg.Blog.Data.Repositories {
    public class UserRepository : EntityBaseRepository<User>, IUserRepository {
        public UserRepository (BlogContext context) : base (context) { }

        public bool isEmailUniq (string email) {
            var user = this.GetSingle(u => u.Email == email);
            return user == null;
        }

        public bool IsUsernameUniq (string username) {
            var user = this.GetSingle(u => u.Username == username);
            return user == null;
        }
    }
}