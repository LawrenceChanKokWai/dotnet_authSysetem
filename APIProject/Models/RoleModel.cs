using System.ComponentModel.DataAnnotations;

namespace APIProject.Models
{
    public class RoleModel
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}
