using aspcorebackend.Dtos.User;
using aspcorebackend.Services;
using aspcorebackend.Utils.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace aspcorebackend.Controllers {
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase {
        private readonly IUserService userService;
        public UserController(IUserService userService) {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers([FromQuery] int? page) {
            Response<object> response = userService.GetAllUsers(page);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserById(int id) {
            Response<object> response = userService.GetUserById(id);
            if (response.Status == "success") {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser(RegisterDTO registerDTO) {
            if (ModelState.IsValid) {
                Response<object> response = await userService.AddUser(registerDTO);
                return StatusCode(201, response);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateUser(int id, UserDTO userDTO) {
            Claim? currentUser = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (currentUser == null || currentUser.Value != id.ToString()) {
                return Unauthorized(ResponseMaker<object>.ErrorRes("You are not authorized to update this user"));
            }

            Response<object> response = userService.UpdateUser(id, userDTO);
            if (response.Status == "success") {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser(int id) {
            Claim? currentUser = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (currentUser == null || currentUser.Value != id.ToString()) {
                return Unauthorized(ResponseMaker<object>.ErrorRes("You are not authorized to delete this user"));
            }

            Response<object> response = userService.DeleteUser(id);
            if (response.Status == "success") {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
