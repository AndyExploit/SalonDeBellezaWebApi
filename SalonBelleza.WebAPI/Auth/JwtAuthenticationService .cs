using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Agregar las siguientes librerias
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SalonBelleza.EntidadesDeNegocio;
//*********************************

namespace SalonBelleza.WebAPI.Auth
{
    public class JwtAuthenticationService : IJwtAuthenticationService 
    {
        private readonly string _key; //Creacion de Llave tipo String que solo debera ser de Lectura y de acceso privado. .

        public JwtAuthenticationService(string key)
        {
            _key = key;
        }

        public string Authenticate(Usuario pUsuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); //Esta varibale nos permite generar un Token.
            var tokenKey = Encoding.ASCII.GetBytes(_key); //
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, pUsuario.Login)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature) //El token tendra el algoritmo HmacSha256
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
