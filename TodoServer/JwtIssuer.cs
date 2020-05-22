using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace TodoServer
{
    public class JwtIssuer
    {
        public static SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("super secret key"));
        public static SigningCredentials signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        private Func<Task<string>> JtiGenerator =>
            () => Task.FromResult(Guid.NewGuid().ToString());

        public async Task<string> GenerateEncodedToken(Guid id, string username)
        {
            List<Claim> claims = await CreateClaims(id, username);

            // foreach (string role in roles)
            // {
            //     claims.Add(new Claim(ClaimTypes.Role, role));
            // }

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(7d),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<List<Claim>> CreateClaims(Guid id, string username)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, await JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUniversalTime().ToString(CultureInfo.InvariantCulture),
                    ClaimValueTypes.Integer64),
                new Claim("Id", id.ToString()),
                new Claim(ClaimTypes.Name, username)
            };
            
            return claims;
        }
    }
}