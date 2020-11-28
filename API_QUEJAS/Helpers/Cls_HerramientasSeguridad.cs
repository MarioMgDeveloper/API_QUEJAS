using API_QUEJAS.ModelosPersonalizados;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API_QUEJAS.Helpers
{
    public class Cls_HerramientasSeguridad
    {
        private readonly IConfiguration _configuration;
        public static string publickey = "santhosh";
        public static string secretkey = "engineer";

        public Cls_HerramientasSeguridad(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetJWT(ClsLogin usuario)
        {

            string result = "";

            try
            {
                var claim = new[]
                {
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Correo),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));


                var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var expiration = DateTime.UtcNow.AddDays(1);


                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _configuration["JWT:issuer"],
                    audience: _configuration["JWT:audience"],
                    claims: claim,
                    expires: expiration,
                    signingCredentials: credenciales
                    );

                result = new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception e)
            {

            }

            return result;
        }
    }
}
