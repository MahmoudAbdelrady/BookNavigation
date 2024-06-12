using aspcorebackend.Dtos.User;
using aspcorebackend.Utils.Response;

namespace aspcorebackend.Services {
    public interface IAuthService {
        Task<Response<object>> Login(LoginDTO loginDTO);
    }
}
