using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Model.Model
{
    public class CustomerOrder
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }

}
