using MW.Domain.Entities.Catalog;
using MW.Domain.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application.Interfaces.Repositories.CustomRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetAllAsync(bool showDeactived = false, bool showDeleted = false);
        public Task<Product> GetByBarcodeAsync(string barcode);
        public Task<int> AddStockAsync(int productId,int quantity);
    }
}
