


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soccer_Care.Models
{
    public class OrderFieldModel
    {
        [Key]
        public string IDOrder { get; set; } 
        public string IDFootballField { get; set; }
        public string Username { get;set; }
        [ForeignKey("Username")]
        public UserModel User { get; set; }
        [ForeignKey("IDFootballField")]
        public FootBallFieldModel FootBall { get; set; }
    }
}
