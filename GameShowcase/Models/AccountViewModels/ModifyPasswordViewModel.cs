using System.ComponentModel.DataAnnotations;

namespace GameShowcase.Models.AccountViewModels
{
    public class ModifyPasswordViewModel
    {
        [Required(ErrorMessage = "此项不可为空")]
        [Display(Name = "原密码")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "密码至少6位")]
        [MaxLength(16, ErrorMessage = "密码最多16位")]
        public string OriginPassword { get; set; }

        [Required(ErrorMessage = "此项不可为空")]
        [Display(Name = "新密码")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "密码至少6位")]
        [MaxLength(16, ErrorMessage = "密码最多16位")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "此项不可为空")]
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "两次密码不一致")]
        [MinLength(6, ErrorMessage = "密码至少6位")]
        [MaxLength(16, ErrorMessage = "密码最多16位")]
        public string ConfirmPassword { get; set; }
    }
}
