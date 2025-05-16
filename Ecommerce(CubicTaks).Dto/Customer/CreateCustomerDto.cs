using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Dto.Customer
{
    public class CreateCustomerDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
