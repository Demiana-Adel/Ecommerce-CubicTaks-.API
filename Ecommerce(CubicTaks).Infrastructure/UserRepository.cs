using Ecommerce_CubicTaks_.Application.Contract;
using Ecommerce_CubicTaks_.Context;
using Ecommerce_CubicTaks_.Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Infrastructure
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }

}
