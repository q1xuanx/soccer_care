using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soccer_Care.Models
{
    public class HistoryOrderModel
    {
        [Key]
        public string IDHistory { get; set; }
        public string Username { get; set; }
        public string IDFootballField { get;set; }
        
        public string IDUser { get; set; }
        [ForeignKey("IDUser")]
        public UserModel User { get; set; }
        [ForeignKey("IDFootballField")]
        public FootBallFieldModel FootBall { get; set; }
    }
}
