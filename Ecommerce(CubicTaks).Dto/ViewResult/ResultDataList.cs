using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Dto.ViewResult
{
    public class ResultDataList<TEntity>
    {
        public List<TEntity> Entities { get; set; }
        public int Count { get; set; }

        public ResultDataList()
        {
            Entities = new List<TEntity>();
        }

        public ResultDataList(List<TEntity> entities)
        {
            Entities = entities ?? new List<TEntity>();
            Count = Entities.Count;
        }

        public ResultDataList(List<TEntity> entities, int count)
        {
            Entities = entities ?? new List<TEntity>();
            Count = count;
        }
    }
}
