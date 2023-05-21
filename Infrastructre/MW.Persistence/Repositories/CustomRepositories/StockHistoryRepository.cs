using Dapper;
using MW.Application.Interfaces.Repositories.CustomRepositories;
using MW.Domain.Entities.Catalog;
using MW.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Persistence.Repositories.CustomRepositories
{
    public class StockHistoryRepository : IStockHistoryRepository
    {
        private readonly MWDbContext _context;
        public StockHistoryRepository(MWDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(StockHistory entity)
        {
            entity.CreatedOn = DateTime.Now;

            var query = "INSERT INTO StockHistories (ProductId, Quantity, Comment, CreatedOn) VALUES (@ProductId, @Quantity, @Comment, @CreatedOn)" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("ProductId", entity.ProductId, DbType.Int64);
            parameters.Add("Quantity", entity.Quantity, DbType.Int64);
            parameters.Add("Comment", entity.Comment, DbType.String);
            parameters.Add("CreatedOn", entity.CreatedOn, DbType.DateTime2);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleAsync<int>(query, parameters);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = "DELETE FROM StockHistories WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { id });
            }
        }
        
        public async Task<IReadOnlyList<StockHistory>> GetAllAsync()
        {
            var query = "SELECT * FROM StockHistories";
            using (var connection = _context.CreateConnection())
            {
                IEnumerable<StockHistory> stockHistory = await connection.QueryAsync<StockHistory>(query);
                return stockHistory.ToList();
            }
        }

        public async Task<StockHistory> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM StockHistories WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<StockHistory>(query, new { id });
            }
        }

        public async Task<int> UpdateAsync(StockHistory entity)
        {
            var query = "UPDATE StockHistories SET " +
                "ProductId = @ProductId," +
                "Quanity = @Quantity," +
                "Comment = @Comment " +
                "WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Int64);
            parameters.Add("ProductId", entity.ProductId, DbType.Int64);
            parameters.Add("Quantity", entity.Quantity, DbType.Int64);
            parameters.Add("Comment", entity.Comment, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
