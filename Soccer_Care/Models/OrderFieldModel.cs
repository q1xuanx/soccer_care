


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soccer_Care.Models
{
    public class OrderFieldModel
    {
        [Key]
        public string IDOrder { get; set; } 
        [Required]
        public string SoDienThoai { get;set; }
        public string IDFootballField { get; set; }
        public string IDChildField { get; set; }
        public string IDUser { get; set; }

        [ForeignKey("IDUser")]
        public UserModel User { get; set; }

        [ForeignKey("IDFootballField")]
        public FootBallFieldModel FootBall { get; set; }

        [ForeignKey("IDChildField")]
        public ListFieldModel ListField { get; set; }
    }
}
