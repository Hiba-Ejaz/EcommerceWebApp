
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Dtos
{
    public class UserCredentialsDto{
        public string   Email   { get; set;}
        public string Password { get; set;}
    }
    public class UserReadDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
       public Role Role { get; set; }   
    }
    public class UserCreateDto
    {

        public string Email { get; set; }
        public string Name { get; set; }
         public string Password { get; set; }
        public string Avatar { get; set; }
      
  
    }
     public class UserUpdateDto
    {

        public string Email { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
      
  
    }
}