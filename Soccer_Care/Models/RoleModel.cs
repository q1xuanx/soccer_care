using System.ComponentModel.DataAnnotations;

namespace Soccer_Care.Models
{
    public class RoleModel
    {
        [Key]
        [Display(Name ="Role ID")]
        public string IDRole { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}