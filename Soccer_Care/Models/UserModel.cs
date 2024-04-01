using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soccer_Care.Models
{
    public class UserModel : IdentityUser
    {
        public string FullName { get;set; }
        public string PhoneNumber { get;set; }
        public string? AvatarURL { get; set; }
        public int IsBlock { get; set; }
    }
}
