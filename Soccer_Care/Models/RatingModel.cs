using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soccer_Care.Models
{
    public class RatingModel
    {
        [Key]
        public string IDDanhGia { get; set; }
        [Required(ErrorMessage ="Không được để trống điểm đánh giá")]
        [Range(1,5)]
        public int Diem { get; set; }
        [Required(ErrorMessage = "Để lại 1 bình luận để chủ sân biết nhé")]
        public string Comments { get; set; }
        public string IDField { get; set; }
        public string Username { get;set; }
        [ForeignKey("Username")]
        public UserModel User { get; set; }
        [ForeignKey("IDField")]
        public FootBallFieldModel FootBall { get; set; }
    }
}
