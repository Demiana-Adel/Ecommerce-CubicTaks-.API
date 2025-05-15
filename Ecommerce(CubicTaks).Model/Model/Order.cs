using Ecommerce_CubicTaks_.Model.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Model.Model
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public  ICollection<CustomerOrder> CustomerOrders { get; set; } = new List<CustomerOrder>();
    }

}
