using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SimemNetAdmin.Domain.Models;
using SimemNetAdmin.Domain.Models.SeguridadTercero;
using SimemNetAdmin.Domain.ViewModel;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimemNetAdmin.Transversal.Helper
{
    [ExcludeFromCodeCoverage]
    public static class JwtHandler
    {
        #region Methods
        public static JwtDataDto ReadJWT(string token)
        {
            JwtDataDto jwtDataDto = new()
            {
                User = "",
                Company = ""
            };

            try
            {
                var secretKey = "Gp7T60-KBj5kMHJI1-kARZzbNE1kt_vlypAxVkMU9CU";
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                var handler = new JwtSecurityTokenHandler();
                var validations = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var claims = handler.ValidateTokenAsync(token, validations);
                if (claims.Result.Claims != null)
                {
                    object user;
                    object company;
                    claims.Result.Claims.TryGetValue("User", out user!);
                    claims.Result.Claims.TryGetValue("Company", out company!);
                    jwtDataDto.User = (string)user;
                    jwtDataDto.Company = (string)company;
                }

                return jwtDataDto;
            }
            catch (Exception)
            {
                return jwtDataDto;
            }
        }
        #endregion


        #region Method
        [ExcludeFromCodeCoverage]
        public static string GenerateJwtToken(string secretKey, GestionUsuarios user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
             {
                new Claim("user", JsonConvert.SerializeObject(user))
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string ValidateToken(string accessToken)
        {
            string response = "";
            // Parse the token
            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(accessToken);

            // Get the expiration time from the token's payload
            DateTime expirationTime = jwtToken.ValidTo;
            // Get the current time
            DateTime currentTime = DateTime.UtcNow;

            // Compare the expiration time to the current time
            //if (currentTime < expirationTime)
            //{

            //}

            string? user = jwtToken.Claims.FirstOrDefault()?.Value;

            response = !string.IsNullOrEmpty(user) ? user : "";

            return response;
        }


        #endregion
    }
}
