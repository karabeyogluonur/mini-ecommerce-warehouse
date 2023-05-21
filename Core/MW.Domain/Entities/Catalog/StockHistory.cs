using MW.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Domain.Entities.Catalog
{
    public class StockHistory : BaseEntity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
