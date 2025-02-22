using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;
using therapist.API.DTOs;
using therapist.API.Helpers;
using Therapist.Core.Models.Identity;
using Therapist.Core.Services;
using Therapist.Services;

namespace therapist.API.Controllers
{
    
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _user;
        private readonly ITokenService _token;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> user , ITokenService token , SignInManager<AppUser> signInManager)
        {
            this._user = user;
            this._token = token;
            this._signInManager = signInManager;
        }
        [HttpGet("IfEmailExists")]
        public async Task<ActionResult<bool>> IfEmailExists(string email)
        {
            var user = await _user.FindByEmailAsync(email);
            if (user == null) return false;
            else return true;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register([FromForm]RegisterDTO registerDTO)
        {
            if (IfEmailExists(registerDTO.Email).Result.Value)
            {
                return BadRequest(new { msg = "email already in use" });
            }

                var user = new AppUser
                {
                    Email = registerDTO.Email,
                    UserName = registerDTO.Email.Split("@")[0],
                    DisName = registerDTO.DisName,
                };

                var result = await _user.CreateAsync(user , registerDTO.Password);

                if (!result.Succeeded)
                {
                    return BadRequest();
                }

            var Returned = new UserDTO
            {
                Email = user.Email,
                DisplayName = user.DisName,
                Token = await _token.GenerateToken(user, _user)
            };
            return Ok(Returned);
            
        }



        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromForm] LoginDTO loginDTO)
        {
            var user = await _user.FindByEmailAsync(loginDTO.Email);
            if (user == null) return Unauthorized();
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password,false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            var returned = new UserDTO
            {
                Email = user.Email,
                DisplayName = user.DisName,
                Token =await _token.GenerateToken(user, _user)
            };

            return Ok(returned);

        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _user.FindByEmailAsync(Email);

            var returned = new UserDTO()
            {
                DisplayName = user.DisName,
                Email = user.Email,
                Token = await _token.GenerateToken(user, _user),
            };
            return Ok(returned);
        }
        [Authorize]
        [HttpPut("changepassword")]
        public async Task<ActionResult<UserDTO>> ChangePassword([FromForm] ChangePasswordDTO changePassword)
        {
           var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _user.FindByEmailAsync(email);
            if(user == null) return NotFound();
            var result = await _user.ChangePasswordAsync(user,changePassword.CurrentPassword,changePassword.NewPassword);
            if(!result.Succeeded) return BadRequest();
            return Ok(new {msg = "password changed succefully"});
        }
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _user.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("User not found.");

            var token = await _user.GeneratePasswordResetTokenAsync(user);

            var resetLink = $"{model.ClientURL}/reset-password?email={model.Email}&token={Uri.EscapeDataString(token)}";
           await  SendEmail.SendEmailAsync(user.Email!, "reset your password in my App", resetLink);
            return Ok(new { Message = "Password reset link sent successfully!" });
        }
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] RestPasswordDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _user.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("Invalid request.");

            var result = await _user.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (result.Succeeded)
                return Ok(new { Message = "Password has been reset successfully!" });

            return BadRequest(result.Errors);
        }

    }
}
