using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Model.Model.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; } = false;

        public DateTime? CreatedAt { get; set; }
    }
}
