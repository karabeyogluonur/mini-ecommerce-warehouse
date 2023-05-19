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
            entity.CreatedOn = DateTime.Now;
            entity.Deleted = false;

            var query = "INSERT INTO Users (FirstName, LastName, Email, PasswordHash, Active, Deleted, AvatarImageName, CreatedOn) VALUES (@FirstName, @LastName, @Email, @PasswordHash, @Active, @Deleted, @AvatarImageName, @CreatedOn)" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", entity.FirstName, DbType.String);
            parameters.Add("LastName", entity.LastName, DbType.String);
            parameters.Add("Email", entity.Email, DbType.String);
            parameters.Add("PasswordHash", entity.PasswordHash, DbType.String);
            parameters.Add("Active", entity.Active, DbType.Boolean);
            parameters.Add("Deleted", entity.Deleted, DbType.Boolean);
            parameters.Add("AvatarImageName", entity.AvatarImageName, DbType.String);
            parameters.Add("CreatedOn", entity.CreatedOn, DbType.DateTime2);

            using (var connection = _context.CreateConnection())
            {
               return await connection.QuerySingleAsync<int>(query, parameters);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = "UPDATE Users SET Deleted = 1, Email = Email + '--DELETED', Active = 0 WHERE Id = @Id";
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

        public async Task<IReadOnlyList<User>> GetAllAsync(bool showDeactived = false, bool showDeleted = false)
        {
            var query = "SELECT * FROM Users ";
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
                IEnumerable<User> users = await connection.QueryAsync<User>(query);
                return users.ToList();
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var query = "SELECT * FROM Users WHERE Email = @Email";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<User>(query, new { email });
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<User>(query, new { id });
            }
        }

        public async Task<int> UpdateAsync(User entity)
        {
            var query = "UPDATE Users SET " +
                "FirstName = @FirstName," +
                "LastName = @LastName," +
                "Email = @Email," +
                "PasswordHash = @PasswordHash," +
                "Active = @Active," +
                "AvatarImageName = @AvatarImageName " +
                "WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Int32);
            parameters.Add("FirstName", entity.FirstName, DbType.String);
            parameters.Add("LastName", entity.LastName, DbType.String);
            parameters.Add("Email", entity.Email, DbType.String);
            parameters.Add("PasswordHash", entity.PasswordHash, DbType.String);
            parameters.Add("Active", entity.Active, DbType.Boolean);
            parameters.Add("AvatarImageName", entity.AvatarImageName, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
