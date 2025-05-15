using Ecommerce_CubicTaks_.Model.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Model.Model
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public  ICollection<CustomerOrder> CustomerOrders { get; set; } = new List<CustomerOrder>();
    }

}
