using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class ProductCreateDTO
    {
        [Required]
        public int? CategoryId { get; set; }
        [Required]
        [MaxLength(40)]
        public string ProductName { get; set; } = null!;
        [Required]
        [MaxLength(20)]
        public string Weight { get; set; } = null!;
        [Required]
        [Range(1, Double.MaxValue)]
        public decimal UnitPrice { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int UnitsInStock { get; set; }
    }
}
