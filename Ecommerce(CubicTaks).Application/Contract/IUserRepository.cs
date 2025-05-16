using Ecommerce_CubicTaks_.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Application.Contract
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
