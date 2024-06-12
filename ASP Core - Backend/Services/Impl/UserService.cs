using aspcorebackend.Dtos.User;
using aspcorebackend.Models;
using aspcorebackend.Repositories;
using aspcorebackend.Utils.Response;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace aspcorebackend.Services.Impl {
    public class UserService : IUserService {
        private readonly IUserRepository userRepository;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        public UserService(IUserRepository userRepository, UserManager<User> userManager, IMapper mapper) {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public Response<object> GetAllUsers(int? page) {
            try {
                List<User> users = userRepository.GetAllUsers(page);
                List<UserDTO> userDTOs = mapper.Map<List<UserDTO>>(users);
                return ResponseMaker<object>.SuccessRes("Users retrieved successfully", userDTOs);
            } catch (Exception) {
                throw new Exception("Failed to retrieve users");
            }
        }

        public Response<object> GetUserById(int id) {
            try {
                User? user = userRepository.GetUserById(id);
                if (user == null) {
                    return ResponseMaker<object>.ErrorRes("User not found");
                }
                UserDTO userDTO = mapper.Map<UserDTO>(user);
                return ResponseMaker<object>.SuccessRes("User retrieved successfully", userDTO);
            } catch (Exception) {
                throw new Exception("Failed to retrieve the user");
            }
        }

        public async Task<Response<object>> AddUser(RegisterDTO registerDTO) {
            try {
                User? existingUser = userRepository.GetUserByUsernameOrEmail(registerDTO.Username, registerDTO.Email);
                if (existingUser != null) {
                    if (existingUser.Email == registerDTO.Email) {
                        return ResponseMaker<object>.ErrorRes("Email already exists");
                    }
                    return ResponseMaker<object>.ErrorRes("Username already exists");
                }

                User user = mapper.Map<User>(registerDTO);
                IdentityResult result = await userManager.CreateAsync(user, registerDTO.Password);
                if (result.Succeeded) {
                    return ResponseMaker<object>.SuccessRes("Account created successfully", "No data");
                }

                return ResponseMaker<object>.ErrorRes("Failed to create a new account", result.Errors);
            } catch (Exception) {
                throw new Exception("Failed to create a new account");
            }
        }

        public Response<object> UpdateUser(int id, UserDTO userDTO) {
            try {
                User? user = userRepository.GetUserById(id);
                if (user == null) {
                    return ResponseMaker<object>.ErrorRes("User not found");
                }
                userDTO.Id = id;
                mapper.Map(userDTO, user);
                userRepository.UpdateUser();
                mapper.Map(user, userDTO);
                return ResponseMaker<object>.SuccessRes("User updated successfully", userDTO);
            } catch (Exception) {
                throw new Exception("Failed to update user information");
            }
        }

        public Response<object> DeleteUser(int id) {
            try {
                User? user = userRepository.GetUserById(id);
                if (user == null) {
                    return ResponseMaker<object>.ErrorRes("User not found");
                }
                userRepository.DeleteUser(user);
                return ResponseMaker<object>.SuccessRes("User deleted successfully", "No data");
            } catch (Exception) {
                throw new Exception("Failed to delete the user");
            }
        }
    }
}
