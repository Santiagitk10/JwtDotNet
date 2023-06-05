using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtApplication.Helpers
{
    public class JwtService : IJwtService
    {
        //La key debe ser una frase larga de lo contrario genera error
        private string secureKey = "this is a very secure key, I swear it is";

        public string Generate(int id)
        {
            //Se convierte la secureKey en una secuencia de Bytes
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            //Se define la firma digital con la Key y se escoge el algoritmo
            var credentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            //Objetos JSON que representan las operaciones criptográficas
            var header = new JwtHeader(credentials);
            //El payload son los claims o lo que yo voy a encriptar, son JSON objects, tiene la duración del token
            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));
            //El token se cigra con
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}