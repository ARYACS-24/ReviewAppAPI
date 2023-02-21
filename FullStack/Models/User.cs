using System.ComponentModel.DataAnnotations;

namespace FullStack.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Roles { get; set; }
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Passwords { get; set; }
        public string Token { get; set; }

    }
}
