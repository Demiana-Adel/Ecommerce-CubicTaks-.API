using Ecommerce_CubicTaks_.Model.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Model.Model
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; } // hashed
        public string Role { get; set; } // e.g., "admin", "user"
    }

}
