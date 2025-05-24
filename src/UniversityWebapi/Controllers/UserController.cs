using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversityWebapi.Database;
using UniversityWebapi.Dtos.User;
using UniversityWebapi.Models;
using UniversityWebapi.Options;
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

        [HttpGet]
        public async Task<IActionResult> CheckToken(string token)
        {
            try
            {
                return Ok(await _jwtTokenService.ReadJwtToken(token));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpGet]
        [Authorize]
        public IActionResult CheckIfAuthorized()
        {
            return Ok("You are authorized");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var foundUser = await _userManager.FindByEmailAsync(dto.Login);

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
                .Select(roleStr => new Claim(ClaimTypes.Role, roleStr))
                .Append(new Claim(ClaimTypes.NameIdentifier, foundUser.Id))
                .Append(new Claim(ClaimTypes.Email, foundUser.Email!))
                .Append(new Claim(ClaimTypes.Name, foundUser.Name))
                .Append(new Claim(ClaimTypes.Surname, foundUser.Surname))
                .Append(new Claim("StudentProfielId", Convert.ToString(foundUser.StudentProfileId)))
                .Append(new Claim("TeacherProfileId", Convert.ToString(foundUser.TeacherProfileId)));

            return Ok(new
            {
                refresh = _jwtTokenService.CreateRefreshToken(foundUser.Id),
                access = _jwtTokenService.CreateAccessToken(claims)
            });
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegisterDto dto)
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
        [HttpPut]
        [ActionName("Token")]
        public async Task<IActionResult> UpdateToken([Required] Guid refresh)
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

            Claim? idClaim = await _jwtTokenService.ReadJwtToken(tokenStr);

            if (idClaim is null)
            {
                return BadRequest($"Token has no claim with type '{ClaimTypes.NameIdentifier}'");
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
                    .Append(new Claim(ClaimTypes.NameIdentifier, foundUser.Id))
                    .Append(new Claim(ClaimTypes.Email, foundUser.Email!))
                    .Append(new Claim(ClaimTypes.Name, foundUser.Name))
                    .Append(new Claim(ClaimTypes.Surname, foundUser.Surname))
                    .Append(new Claim("StudentProfielId", Convert.ToString(foundUser.StudentProfileId)))
                    .Append(new Claim("TeacherProfileId", Convert.ToString(foundUser.TeacherProfileId)));
                return Ok(new
                {
                    access = _jwtTokenService.CreateAccessToken(claims),
                    refresh = await _jwtTokenService.UpdateRefreshToken(refresh, foundUser.Id)
                });
            }
            catch (SecurityTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}