using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerceApi.Data.Options;
using ECommerceApi.Service.IService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceApi.Service.Service;

public class TokenService : ITokenService
{
    private readonly SecretOptions _secrets;
    private readonly ILogger<TokenService> _logger;

    public TokenService( IOptions<SecretOptions> secrets, ILogger<TokenService> logger)
    {
        _secrets = secrets.Value;
        _logger = logger;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        _logger.LogInformation("Starting access token generation");
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secrets.JWT.Key));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            issuer: _secrets.JWT.Issuer,
            audience: _secrets.JWT.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(GetAccessTokenExpirationMinutes()),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        _logger.LogInformation("Access token generated successfully");
        return tokenString;
    }

    public string GenerateRefreshToken()
    {
        _logger.LogInformation("Generating refresh token");
        var randomNumber = new byte[32];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        var refreshToken = Convert.ToBase64String(randomNumber);
        _logger.LogInformation("Refresh token generated successfully");
        return refreshToken;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        _logger.LogInformation("Extracting principal from expired token");
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secrets.JWT.Key)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            _logger.LogWarning("Invalid token");
            throw new SecurityTokenException("Invalid token");
        }

        _logger.LogInformation("Principal extracted successfully from expired token");
        return principal;
    }

    public int GetAccessTokenExpirationMinutes()
    {
        return _secrets.JWT.AccessTokenExpirationMinutes;
    }
}