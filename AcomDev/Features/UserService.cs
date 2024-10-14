using AcomDev.Common;
using AcomDev.Data;
using AcomDev.Models;
using Dapper;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace AcomDev.Features
{
    public class UserService
    {
        private readonly IConfiguration _configuration;

        private readonly APIContext _context;
        public UserService(APIContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private readonly string? _connectionString;

        public async Task<IEnumerable<User>> GetUsersByCompanyId(string companyId)
        {
            using var connection = _context.CreateConnection();

            const string sql = """ SELECT * FROM users WHERE CompanyId= @companyId""";
            return await connection.QueryAsync<User>(sql, param: new { companyId });
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            using var connection = _context.CreateConnection();

            const string sql = """ SELECT * FROM users WHERE Email = @email """;

            return await connection.QuerySingleOrDefaultAsync<User>(sql, param: new { email });
        }

        public async Task<int> AddUserAsync(User user)
        {
            using var connection = _context.CreateConnection();

            var newpassword = PasswordGenerator.GenerateRandomPassword(15);
            var SHApassword = ComputeSha256HashWithSalt(newpassword, "AcomDev");

            user.Password = SHApassword;
            user.temppass = newpassword;
            const string sql = """ INSERT INTO users (CompanyId, Title, Name, MobileNumber, Email, Password, Role, DrTeamName, DrSkill, Address, Street, City, State, ZipCode, Country, EmergencyContactName, EmergencyContactEmail, EmergencyContactPhone, EmergencyContactRelationship, temppass) VALUES (@companyId, @title, @name, @mobilenumber, @email, @password, @role, @drteamname, @drskill, @address, @street, @city, @state, @zipcode, @country, @emergencyContactName, @emergencyContactEmail, @emergencyContactPhone, @emergencyContactRelationship, @temppass) """;

            var result = await connection.ExecuteAsync(sql, user);
            SendMail(user.Email, user.Name, newpassword);

            return result;
        }

        public async Task<int> UpdateUserAsync(UpdateUser updateuser)
        {
            using var connection = _context.CreateConnection();

            const string sql = """ UPDATE users Set Title=@title, Name=@name, MobileNumber=@mobilenumber, Email=@email, Address=@address, Street=@street, City=@city, State=@state, ZipCode=@zipcode, Country=@country, IsActive=@isactive Where Id=@id """;

            var result = await connection.ExecuteAsync(sql, updateuser);
            
            return result;
        }

        public string ComputeSha256HashWithSalt(string rawData, string salt)
        {
            // Combine password with salt
            string saltedData = rawData + salt;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(saltedData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public async Task SendMail(string email, string name, string password)
        {
            var apiKey = _configuration.GetValue<string>("SendGridKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ranjana_g@trigent.com", "NoReply");
            var subject = "AcomDev Password";
            var to = new EmailAddress(email, name);
            var plainTextContent = password;
            var htmlContent = "<strong>Test Mail</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
