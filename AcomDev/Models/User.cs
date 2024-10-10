using System.ComponentModel.DataAnnotations.Schema;

namespace AcomDev.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Name { get; set; } = string.Empty;
        public string? DRTeamName { get; set; } = string.Empty;
        public string? DRTeamSkill { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public string? MobilePhone { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;        
        public string? Address { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public string? ZipCode { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;

    }
}
