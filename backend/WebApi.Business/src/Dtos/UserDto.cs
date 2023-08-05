
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
       public Role Role { get; set; }   
    }
}