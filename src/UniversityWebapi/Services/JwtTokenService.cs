using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversityWebapi.Models;
using UniversityWebapi.Options;
using UniversityWebapi.Repositories;

namespace UniversityWebapi.Services
{
    public class JwtTokenService(JwtTokenRepository jwtTokenRepository, IOptionsSnapshot<JwtOptions> jwtOptionsSnapshot)
    {
        readonly JwtTokenRepository _jwtTokenRepository = jwtTokenRepository;
        readonly JwtOptions _jwtOptions = jwtOptionsSnapshot.Value;

        public async Task<RefreshToken> UpdateRefreshTokenAsync(Guid token, string userId)
        {
            var refreshToken = await _jwtTokenRepository.GetRefreshToken(token, userId) ?? throw new KeyNotFoundException("Refresh token not found!");
            await _jwtTokenRepository.DeleteRefreshToken(refreshToken);
            return await CreateRefreshTokenAsync(userId);
        }
        public Claim? ReadIdFromJwtToken(string tokenStr)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenStr);
            Claim? idClaim = token.Claims.FirstOrDefault(claim => claim.Type == "Id");
            return idClaim;
        }

        public string CreateAccessToken(IEnumerable<Claim> claims)
        {
            var signingKey = new SymmetricSecurityKey(_jwtOptions.KeyInBytes);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTimeInMinutes),
                signingCredentials: signingCredentials
            );

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }

        public async Task<RefreshToken> CreateRefreshTokenAsync(string userId)
        {
            var token = Guid.NewGuid();
            var refreshToken = new RefreshToken
            {
                Token = token,
                UserId = userId
            };
            await _jwtTokenRepository.CreateRefreshToken(refreshToken);
            return refreshToken;
        }
    }
}
