using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;
using UniversityWebapi.Dtos.User;
using UniversityWebapi.Models;
using UniversityWebapi.Services;

namespace UniversityWebapi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class UserController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            JwtTokenService jwtTokenService
        ) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly JwtTokenService _jwtTokenService = jwtTokenService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var foundUser = await _userManager.FindByEmailAsync(dto.Email);

            if (foundUser == null)
            {
                return base.BadRequest("Incorrect Login or Password");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(foundUser, dto.Password, false, false);


            if (signInResult.Succeeded == false)
            {
                return base.BadRequest("Incorrect Login or Password");
            }

            var roles = await _userManager.GetRolesAsync(foundUser);

            var claims = roles
                .Select(roleStr => new Claim("Role", roleStr))
                .Append(new Claim("Id", foundUser.Id))
                .Append(new Claim("Email", foundUser.Email!))
                .Append(new Claim("Name", foundUser.Name))
                .Append(new Claim("Surname", foundUser.Surname))
                .Append(new Claim("ProfilePictureUrl", foundUser.ProfilePictureUrl ?? ""))
                .Append(new Claim("StudentProfileId", Convert.ToString(foundUser.StudentProfileId) ?? ""))
                .Append(new Claim("TeacherProfileId", Convert.ToString(foundUser.TeacherProfileId) ?? ""));

            return Ok(new
            {
                refresh = (await _jwtTokenService.CreateRefreshTokenAsync(foundUser.Id)).Token,
                access = _jwtTokenService.CreateAccessToken(claims)
            });
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                UserName = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname,
            };

            var result = await _userManager.CreateAsync(newUser, dto.Password);

            if (!result.Succeeded)
            {
                return base.BadRequest(result.Errors);
            }

            return Ok();
        }
        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(UserUpdateDto dto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (userId is null)
            {
                return base.StatusCode(401);
            }

            User? foundUser = await _userManager.FindByIdAsync(userId);

            if (foundUser is null)
            {
                return BadRequest($"User not found by id: '{userId}'");
            }

            if (!await _userManager.CheckPasswordAsync(foundUser, dto.Password))
            {
                return BadRequest("Incorrect password");
            }

            foundUser.Name = dto.Name ?? foundUser.Name;
            foundUser.Surname = dto.Surname ?? foundUser.Surname;
            foundUser.ProfilePictureUrl = dto.ProfilePictureUrl ?? foundUser.ProfilePictureUrl;

            if (!string.IsNullOrEmpty(dto.NewPassword))
            {
                var passwordResult = await _userManager.ChangePasswordAsync(foundUser, dto.Password, dto.NewPassword);
                if (!passwordResult.Succeeded)
                {
                    return base.BadRequest(passwordResult.Errors);
                }
            }

            var result = await _userManager.UpdateAsync(foundUser);

            if (!result.Succeeded)
            {
                return base.BadRequest(result.Errors);
            }

            return Ok();
        }
        [HttpPut]
        [ActionName("Refresh")]
        public async Task<IActionResult> UpdateToken([Required] Guid token)
        {
            var tokenStr = HttpContext.Request.Headers.Authorization.FirstOrDefault();

            if (tokenStr is null)
            {
                return base.StatusCode(401);
            }

            if (tokenStr.StartsWith("Bearer "))
            {
                tokenStr = tokenStr["Bearer ".Length..];
            }

            Claim? idClaim = _jwtTokenService.ReadIdFromJwtToken(tokenStr);

            if (idClaim is null)
            {
                return BadRequest($"Token has no claim with type 'Id'");
            }

            var userId = idClaim.Value;

            User? foundUser = await _userManager.FindByIdAsync(userId);

            if (foundUser is null)
            {
                return BadRequest($"User not found by id: '{userId}'");
            }

            try
            {
                var roles = await _userManager.GetRolesAsync(foundUser);
                var claims = roles
                    .Select(roleStr => new Claim(ClaimTypes.Role, roleStr))
                    .Append(new Claim("Id", foundUser.Id))
                    .Append(new Claim("Email", foundUser.Email!))
                    .Append(new Claim("Name", foundUser.Name))
                    .Append(new Claim("Surname", foundUser.Surname))
                    .Append(new Claim("ProfilePictureUrl", foundUser.ProfilePictureUrl ?? ""))
                    .Append(new Claim("StudentProfileId", Convert.ToString(foundUser.StudentProfileId) ?? ""))
                    .Append(new Claim("TeacherProfileId", Convert.ToString(foundUser.TeacherProfileId) ?? ""));
                return Ok(new
                {
                    access = _jwtTokenService.CreateAccessToken(claims),
                    refresh = (await _jwtTokenService.UpdateRefreshTokenAsync(token, foundUser.Id)).Token
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}