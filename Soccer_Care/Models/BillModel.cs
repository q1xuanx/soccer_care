using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soccer_Care.Models
{
    public class BillModel
    {
        [Key]
        public string IDBill { get; set; }
        [Required]  
        public float TotalPay { get; set; }
        [Required]
        public float TotalTimeUse { get; set; }
        [Required]
        public string IDOrderDetails { get; set; }

        [ForeignKey("IDOrderDetails")]
        public DetailsOrderModel DetailsOrder { get; set; }
    }
}
