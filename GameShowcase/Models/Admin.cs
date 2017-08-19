using System.ComponentModel.DataAnnotations;

namespace GameShowcase.Models
{
    public partial class Admin
    {
        public int Id { get; set; }

        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [MaxLength(16, ErrorMessage = "密码最长16位")]
        [MinLength(6, ErrorMessage = "密码至少6位")]
        public string Password { get; set; }
    }
}
