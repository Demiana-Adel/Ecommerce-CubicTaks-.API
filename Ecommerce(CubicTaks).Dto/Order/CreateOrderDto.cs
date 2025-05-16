using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Dto.Order
{
    public class CreateOrderDto
    {
        [Required]
        public DateTime OrderDate { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        [Required]
        [MinLength(1)]
        public List<int> CustomerIds { get; set; }
    }
}