using aspcorebackend.Dtos.User;
using aspcorebackend.Services;
using aspcorebackend.Utils.Response;
using Microsoft.AspNetCore.Mvc;

namespace aspcorebackend.Controllers {
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService) {
            this.authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO) {
            if (ModelState.IsValid) {
                Response<object> response = await authService.Login(loginDTO);
                if (response.Status == "success") {
                    return Ok(response);
                }
                return Unauthorized(response);
            }
            return BadRequest(ResponseMaker<object>.ErrorRes("Validation Error", ModelState));
        }
    }
}
