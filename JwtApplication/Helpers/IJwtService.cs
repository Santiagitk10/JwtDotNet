using System.IdentityModel.Tokens.Jwt;

namespace JwtApplication.Helpers
{
    public interface IJwtService
    {
        string Generate(int id);

        JwtSecurityToken Verify(string jwt);
    }
}