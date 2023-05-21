using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application.Models.Catalog
{
    public class ProductListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public double PurchasePrice { get; set; }
        public double SalePrice { get; set; }
        public int Stock { get; set; }
        public string ProductImageName { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
