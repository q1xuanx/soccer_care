using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soccer_Care.Models
{
    public class FieldLikeModel
    {
        [Key]
        public string IDFieldLike { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string IDFootballField { get; set; }
        [ForeignKey("IDFootballField")]
        public FootBallFieldModel Football { get; set; }
        [ForeignKey("Username")]
        public UserModel User { get; set; }
    }
}
