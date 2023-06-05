using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace JwtApplication.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        //SE usa JsonIgnore para no devolver en este caso la contraseña del usuario cuando se envíe la respuesta http
        [JsonIgnore] public string Password { get; set; }
    }
}