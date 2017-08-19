using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameShowcase.Models.AccountViewModels
{
    public class AdminLoginViewModel
    {
        [DisplayName("密码")]
        [Required(ErrorMessage = "密码不可为空")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "密码至少6位")]
        public string Password { get; set; }
    }
}
