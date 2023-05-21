using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application.Models.Catalog
{
    public class StockHistoryAddModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Comment { get; set; }
    }
}
