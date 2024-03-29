using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soccer_Care.Models
{
    public class DetailsOrderModel
    {
        [Key]
        public string IDDetails {get; set; }
        public string IDOrder {get; set; }
        [Required(ErrorMessage ="Chọn ngày giờ bắt đầu")]
        public String StartTime { get; set; }
        [Required(ErrorMessage ="Vui lòng cung cấp giờ kết thúc")]
        public DateTime DateTime { get; set; }

        [ForeignKey("IDOrder")]
        public OrderFieldModel Order { get; set; }   
    }
}
