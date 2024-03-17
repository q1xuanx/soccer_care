using System.ComponentModel.DataAnnotations;

namespace Soccer_Care.Models
{
    public class TypeFieldModel
    {
        [Key]
        [Display(Name = "ID loại")]
        public string IDType { get; set; }
        [Required]
        public string NameType { get; set; }
    }
}