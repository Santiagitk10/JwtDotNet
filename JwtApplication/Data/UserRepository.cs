using JwtApplication.Models;

namespace JwtApplication.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            //Me retorna el id del usuario guardado, lo asigno al usuario que guardé y lo retorno
            _context.SaveChanges();
            return user;
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email.Equals(email));
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id.Equals(id));
        }
    }
}