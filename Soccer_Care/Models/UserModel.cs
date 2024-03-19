using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soccer_Care.Models
{
    public class UserModel
    {
        [Required,StringLength(100)]
        [Key]
        public string Username { get; set; }
        [Required, StringLength(20, ErrorMessage ="Password không được ít hơn 10 ký tự"), MinLength(10)]
        public string Password { get; set; }
        [Required]
        public string FullName { get;set; }
        [Required, Phone(ErrorMessage ="Không đúng định dạng số điện thoại")]
        public string PhoneNumber { get; set; }
        public string? AvatarURL { get; set; }
        
        public int IsBlock { get;set; }

        public string IDRole { get; set; }
        [ForeignKey("IDRole")]
        public RoleModel Role { get; set; }
    }
}
