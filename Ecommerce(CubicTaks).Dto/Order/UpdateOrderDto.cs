using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Dto.Order
{
    public class UpdateOrderDto
    {
        [Required]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<int> CustomerIds { get; set; } = new List<int>();
    }
}
