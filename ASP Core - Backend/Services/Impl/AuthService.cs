using aspcorebackend.Dtos.User;
using aspcorebackend.Models;
using aspcorebackend.Repositories;
using aspcorebackend.Utils.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace aspcorebackend.Services.Impl {
    public class AuthService : IAuthService {
        private readonly IUserRepository userRepository;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        public AuthService(IUserRepository userRepository, UserManager<User> userManager, IConfiguration configuration) {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<Response<object>> Login(LoginDTO loginDTO) {
            try {
                User? user = userRepository.GetUserByUsernameOrEmail(loginDTO.UsernameOrEmail);
                if (user == null) {
                    return ResponseMaker<object>.ErrorRes("Invalid credentials");
                }
                bool match = await userManager.CheckPasswordAsync(user, loginDTO.Password);
                if (!match) {
                    return ResponseMaker<object>.ErrorRes("Invalid credentials");
                }
                // @TODO: Implement JWT token generation
                SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!));
                SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

                List<Claim> claims = [];
                claims.Add(new Claim("id", user.Id.ToString()!));
                claims.Add(new Claim("username", user.UserName!));
                claims.Add(new Claim("role", "User"));

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: configuration["JWT:Issuer"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials
                    );

                LoggedUserDTO loggedUserDTO = new LoggedUserDTO {
                    Username = user.UserName!,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };

                return ResponseMaker<object>.SuccessRes("Login successful", loggedUserDTO);
            } catch (Exception) {
                throw new Exception("Failed to login");
            }
        }
    }
}
