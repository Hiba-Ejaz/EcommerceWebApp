using System.Text.Json.Serialization;

namespace WebApi.Domain.src.Entities
{
   [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Customer,
        Admin
    }
    public class User:BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public Role Role { get; set; }   
    }
}