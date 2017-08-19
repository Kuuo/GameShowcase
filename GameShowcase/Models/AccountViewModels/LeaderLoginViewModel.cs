using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GameShowcase.Models.AccountViewModels
{
    public class LeaderLoginViewModel
    {
        [DisplayName("学号")]
        [Required(ErrorMessage = "学号不可为空")]
        [MaxLength(10, ErrorMessage = "学号至多10位")]
        public string StuNumber { get; set; }

        [DisplayName("密码")]
        [Required(ErrorMessage = "密码不可为空")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "密码至少6位")]
        public string Password { get; set; }
    }
}
