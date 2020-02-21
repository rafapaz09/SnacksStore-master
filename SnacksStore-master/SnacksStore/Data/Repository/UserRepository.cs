using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SnacksStore.Data.Interfaces;
using SnacksStore.Data.Model;
using SnacksStore.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SnacksStore.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppSettings _appSettings;

        public UserRepository(SnacksStoreContext context, IOptions<AppSettings> appSettings) : base(context)
        {
            _appSettings = appSettings.Value;
        }

        public IEnumerable<User> GetAllWithRol()
        {
            return _context.Users.Include(u => u.Rol);
        }

        public User GetByIdWithRol(int id)
        {
            return _context.Users.Where(u => u.Id == id).Include(u => u.Rol).SingleOrDefault();
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users.Include(r => r.Rol).SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Rol.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

       
    }
}
