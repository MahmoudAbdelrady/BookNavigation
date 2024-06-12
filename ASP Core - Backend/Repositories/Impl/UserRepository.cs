using aspcorebackend.Config;
using aspcorebackend.Models;

namespace aspcorebackend.Repositories.Impl {
    public class UserRepository : IUserRepository {
        private readonly BookNavDbContext dbContext;
        public UserRepository(BookNavDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public List<User> GetAllUsers(int? page) {
            if (page < 1 || page == null) page = 1;
            int skip = (page.Value - 1) * 10;
            List<User> users = dbContext.Users.Skip(skip).Take(10).ToList();
            return users;
        }

        public User? GetUserById(int id) {
            User? user = dbContext.Users.FirstOrDefault(u => u.Id == id);
            return user;
        }

        public User? GetUserByUsernameOrEmail(string usernameOrEmail) {
            User? user = dbContext.Users.FirstOrDefault(u => u.UserName == usernameOrEmail || u.Email == usernameOrEmail);
            return user;
        }

        public User? GetUserByUsernameOrEmail(string username, string email) {
            User? user = dbContext.Users.FirstOrDefault(u => u.UserName == username || u.Email == email);
            return user;
        }

        public User? GetUserByUsername(string username) {
            User? user = dbContext.Users.FirstOrDefault(u => u.UserName == username);
            return user;
        }

        public User? GetUserByEmail(string email) {
            User? user = dbContext.Users.FirstOrDefault(u => u.Email == email);
            return user;
        }

        public User CreateUser(User user) {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return user;
        }

        public void UpdateUser() {
            dbContext.SaveChanges();
        }

        public void DeleteUser(User user) {
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }
    }
}
