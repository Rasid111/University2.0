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
        private readonly JwtTokenRepository _jwtTokenRepository = jwtTokenRepository;
        private readonly JwtOptions _jwtOptions = jwtOptionsSnapshot.Value;

        public async Task<RefreshToken> UpdateRefreshToken(Guid token, string userId)
        {
            var refreshToken = await _jwtTokenRepository.GetRefreshToken(token, userId) ?? throw new KeyNotFoundException("Refresh token not found!");
            await _jwtTokenRepository.DeleteRefreshToken(refreshToken);
            return await CreateRefreshToken(userId);
        }
        public async Task<Claim?> ReadJwtToken(string tokenStr)
        {
            var handler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtOptions.Audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_jwtOptions.KeyInBytes)
            };
            var validationResult = await handler.ValidateTokenAsync(tokenStr, validationParameters);
            if (!validationResult.IsValid)
            {
                throw new SecurityTokenException("Invalid token");
            }
            var token = handler.ReadJwtToken(tokenStr);
            Claim? idClaim = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
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

        public async Task<RefreshToken> CreateRefreshToken(string userId)
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
