using aspcorebackend.Dtos.User;
using aspcorebackend.Utils.Response;

namespace aspcorebackend.Services {
    public interface IUserService {
        Response<object> GetAllUsers(int? page);
        Response<object> GetUserById(int id);
        Task<Response<object>> AddUser(RegisterDTO registerDTO);
        Response<object> UpdateUser(int id, UserDTO userDTO);
        Response<object> DeleteUser(int id);
    }
}
