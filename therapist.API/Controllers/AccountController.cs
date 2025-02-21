using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using therapist.API.DTOs;
using Therapist.Core.Models.Identity;
using Therapist.Core.Services;

namespace therapist.API.Controllers
{
    
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _user;
        private readonly ITokenService _token;

        public AccountController(UserManager<AppUser> user , ITokenService token)
        {
            this._user = user;
            this._token = token;
        }
        [HttpGet("IfEmailExists")]
        public async Task<ActionResult<bool>> IfEmailExists(string email)
        {
            var user = await _user.FindByEmailAsync(email);
            if (user == null) return false;
            else return true;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register([FromBody]RegisterDTO registerDTO)
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
    }
}
