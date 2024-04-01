using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soccer_Care.Models
{
    public class FootBallFieldModel
    {
        [Key]
        [Display(Name ="ID sân bóng")]
        public string IDFootBallField { get; set; }

        [Required]
        [Display(Name="Tên sân bóng")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Địa chỉ")]
        public string Address { get; set; }
        [Required]
        public string HinhAnhDaiDien { get; set; }
        [Required]
        [Display(Name = "Giá")]
        public float Gia { get; set; }

        public List<ListFieldModel> ListField { get; set; }
        public FootBallFieldModel()
        {
            this.ListField = new List<ListFieldModel>();
        }

        //Thêm khóa ngoại
        public string Username { get; set; }
        public string IDUserOwner { get; set; }
        [ForeignKey("IDUserOwner")]
        public UserModel User { get; set; }

    }
}
