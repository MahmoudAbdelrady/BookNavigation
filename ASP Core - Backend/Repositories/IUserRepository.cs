using aspcorebackend.Models;

namespace aspcorebackend.Repositories {
    public interface IUserRepository {
        List<User> GetAllUsers(int? page);
        User? GetUserById(int id);
        User? GetUserByUsernameOrEmail(string usernameOrEmail);
        User? GetUserByUsernameOrEmail(string username, string email);
        User? GetUserByUsername(string username);
        User? GetUserByEmail(string email);
        User CreateUser(User user);
        void UpdateUser();
        void DeleteUser(User user);
    }
}
