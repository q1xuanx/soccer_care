using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soccer_Care.Models
{
    public class ListFieldModel
    {
        [Key]
        public string IDField { get; set; }
        [Required]
        public string NameField { get; set; }
        [Required]   
        public string DescriptionField { get; set; }
        [Required]
        [Display(Name = "Hình Ảnh")]
        public string HinhAnhSanBong { get; set; }
        public int isDisable { get; set; }
        [NotMapped]
        public IFormFile? FileImage { get; set; }
        public float Gia { get; set; }
        //Add foreign key
        public string IDFootballField { get; set; }
        [ForeignKey("IDFootballField")]
        public FootBallFieldModel FootBall { get; set; }
        public string IDType { get; set; }

        [ForeignKey("IDType")]
        public TypeFieldModel Type { get; set; }
    }
}
