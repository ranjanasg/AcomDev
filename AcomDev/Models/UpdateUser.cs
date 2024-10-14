using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AcomDev.Models
{
    public class UpdateUser
    {
        public int Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? MobileNumber { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public string? ZipCode { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
    }
}
