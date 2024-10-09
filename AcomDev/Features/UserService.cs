using AcomDev.Data;
using AcomDev.Models;
using Dapper;
using System.Security.Cryptography;
using System.Text;

namespace AcomDev.Features
{
    public class UserService
    {
        private readonly APIContext _context;

        public UserService(APIContext context) {  _context = context; }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            using var connection = _context.CreateConnection();

            const string sql = "SELECT * FROM users";
            return await connection.QueryAsync<User>(sql);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            using var connection = _context.CreateConnection();

            const string sql = """ SELECT * FROM Users WHERE Email = @email """;

            return await connection.QuerySingleOrDefaultAsync<User>(sql, param: new { email });
        }

        public async Task<int> AddUserAsync(User user)
        {
            using var connection = _context.CreateConnection();

            const string sql = """ INSERT INTO Users (CompanyId, Name, MobileNumber, Email, Password, Title, DrTeamName, DrSkill, Address, Street, City, ZipCode, Role, Country) VALUES (@companyId, @name, @mobilenumber, @email, @password, @title, @drteamname, @drskill, @address, @street, @city, @zipcode, @role, @country) """;

            var result = await connection.ExecuteAsync(sql, user);
            return result;
        }
    }
}
