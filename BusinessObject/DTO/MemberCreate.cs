using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class MemberCreate
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string CompanyName { get; set; } = null!;
        [Required]
        [StringLength(15)]
        public string City { get; set; } = null!;
        [Required]
        [StringLength(15)]
        public string Country { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; } = null!;
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Comfirm Password does not match")]
        public string? ConfirmPassword { get; set; } = null!;    

    }
}
