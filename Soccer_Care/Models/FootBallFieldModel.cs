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
        [Display(Name = "Giá")]
        public float Gia { get; set; }
        [Required]
        [Display(Name="Hình Ảnh")]
        public string HinhAnhSanBong { get; set; }

        //Thêm khóa ngoại
        public string Username { get; set; }
        public string IDType { get; set; }

        [ForeignKey("IDType")]
        public TypeFieldModel Type { get; set; }
        [ForeignKey("Username")]
        public UserModel User { get; set; }

    }
}
