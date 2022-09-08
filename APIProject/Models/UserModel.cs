using System;
using System.ComponentModel.DataAnnotations;

namespace APIProject.Models
{
    public class UserModel : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int RoleId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
