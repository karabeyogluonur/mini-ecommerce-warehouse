using Dapper;
using MW.Application.Interfaces.Repositories.CustomRepositories;
using MW.Domain.Entities.Catalog;
using MW.Domain.Entities.Membership;
using MW.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MW.Persistence.Repositories.CustomRepositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MWDbContext _context;
        public ProductRepository(MWDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Product entity)
        {
            entity.CreatedOn = DateTime.Now;
            entity.Deleted = false;
            entity.Stock = 0;

            var query = "INSERT INTO Products (Name, Description, Barcode, PurchasePrice, SalePrice, Stock, WarehouseCode, ProductImageName, Active, Deleted, CreatedOn) VALUES (@Name, @Description, @Barcode, @PurchasePrice, @SalePrice, @Stock, @WarehouseCode, @ProductImageName, @Active, @Deleted, @CreatedOn)" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", entity.Name, DbType.String);
            parameters.Add("Description", entity.Description, DbType.String);
            parameters.Add("Barcode", entity.Barcode, DbType.String);
            parameters.Add("PurchasePrice", entity.PurchasePrice, DbType.Double);
            parameters.Add("SalePrice", entity.SalePrice, DbType.Double);
            parameters.Add("Stock", entity.Stock, DbType.Int64);
            parameters.Add("WarehouseCode", entity.WarehouseCode, DbType.String);
            parameters.Add("ProductImageName", entity.ProductImageName, DbType.String);
            parameters.Add("Active", entity.Active, DbType.Boolean);
            parameters.Add("Deleted", entity.Deleted, DbType.Boolean);
            parameters.Add("CreatedOn", entity.CreatedOn, DbType.DateTime2);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleAsync<int>(query, parameters);
            }
        }

        public async Task<int> AddStockAsync(int productId,int quantity)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", productId, DbType.Int64);
            parameters.Add("Quantity", quantity, DbType.Int64);

            var query = "UPDATE Products SET Stock = Stock + @Quantity WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query,parameters);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = "UPDATE Products SET Deleted = 1, Barcode = Barcode + '--DELETED', Active = 0 WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            var query = "SELECT * FROM Products";
            using (var connection = _context.CreateConnection())
            {
                IEnumerable<Product> products = await connection.QueryAsync<Product>(query);
                return products.ToList();
            }
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(bool showDeactived = false, bool showDeleted = false)
        {
            var query = "SELECT * FROM Products ";
            if (!showDeactived)
                query = query + "WHERE Active = 0 ";

            if (!showDeleted)
            {
                if (query.Contains("WHERE"))
                    query = query + "&& Deleted = 0 ";
                else
                    query = query + "WHERE Deleted = 0 ";
            }

            using (var connection = _context.CreateConnection())
            {
                IEnumerable<Product> products = await connection.QueryAsync<Product>(query);
                return products.ToList();
            }
        }

        public async Task<Product> GetByBarcodeAsync(string barcode)
        {
            var query = "SELECT * FROM Products WHERE Barcode = @Barcode";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Product>(query, new { barcode });
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Product>(query, new { id });
            }
        }

        public async Task<int> UpdateAsync(Product entity)
        {
            var query = "UPDATE Products SET " +
                "Name = @Name," +
                "Description = @Description," +
                "Barcode = @Barcode," +
                "PurchasePrice = @PurchasePrice," +
                "SalePrice = @SalePrice," +
                "WarehouseCode = @WarehouseCode, " +
                "ProductImageName = @ProductImageName, " +
                "Active = @Active " +
                "WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Int64);
            parameters.Add("Name", entity.Name, DbType.String);
            parameters.Add("Description", entity.Description, DbType.String);
            parameters.Add("Barcode", entity.Barcode, DbType.String);
            parameters.Add("PurchasePrice", entity.PurchasePrice, DbType.Double);
            parameters.Add("SalePrice", entity.SalePrice, DbType.Double);
            parameters.Add("WarehouseCode", entity.WarehouseCode, DbType.String);
            parameters.Add("ProductImageName", entity.ProductImageName, DbType.String);
            parameters.Add("Active", entity.Active, DbType.Boolean);

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
