using Dapper;
using MW.Application.Interfaces.Repositories.CustomRepository;
using MW.Domain.Entities.Membership;
using MW.Persistence.Contexts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MW.Persistence.Repositories.CustomRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MWDbContext _context;
        public UserRepository(MWDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(User entity)
        {
            var query = "INSERT INTO Users (FirstName, LastName, Email, PasswordHash, Active, Deleted, CreatedOn) VALUES (@FirstName, @LastName, @Email, @PasswordHash, @Active, @Deleted, @CreatedOn)" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", entity.FirstName, DbType.String);
            parameters.Add("LastName", entity.LastName, DbType.String);
            parameters.Add("Email", entity.Email, DbType.String);
            parameters.Add("PasswordHash", entity.PasswordHash, DbType.String);
            parameters.Add("Active", entity.Active, DbType.Boolean);
            parameters.Add("Deleted", entity.Deleted, DbType.Boolean);
            parameters.Add("CreatedOn", entity.CreatedOn, DbType.DateTime2);

            using (var connection = _context.CreateConnection())
            {
               return await connection.QuerySingleAsync<int>(query, parameters);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = "UPDATE Users SET Deleted = 1 WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
               return await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            var query = "SELECT * FROM Users";
            using (var connection = _context.CreateConnection())
            {
                IEnumerable<User> users = await connection.QueryAsync<User>(query);
                return users.ToList();
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleAsync<User>(query, new { id });
            }
        }

        public async Task<int> UpdateAsync(User entity)
        {
            var query = "UPDATE Users SET " +
                "FirstName = @FirstName," +
                "LastName = @LastName," +
                "Email = @Email," +
                "PasswordHash = @PasswordHash," +
                "Active = @Active " +
                "WHERE Id = @Id";

            Console.Write(query);

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Int32);
            parameters.Add("FirstName", entity.FirstName, DbType.String);
            parameters.Add("LastName", entity.LastName, DbType.String);
            parameters.Add("Email", entity.Email, DbType.String);
            parameters.Add("PasswordHash", entity.PasswordHash, DbType.String);
            parameters.Add("Active", entity.Active, DbType.Boolean);

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
