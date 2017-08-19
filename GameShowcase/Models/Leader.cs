using GameShowcase.Areas.Manage.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameShowcase.Models
{
    public partial class Leader
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "学号不可为空")]
        [Display(Name = "学号")]
        [MaxLength(10, ErrorMessage = "学号最长10位")]
        public string StudentNumber { get; set; }

        [Required(ErrorMessage = "姓名不可为空")]
        [Display(Name = "姓名")]
        [MaxLength(10, ErrorMessage = "姓名最长10个字符")]
        public string Name { get; set; }

        [Required(ErrorMessage = "年份不可为空")]
        [Display(Name = "年份")]
        public int Year { get; set; }

        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [MaxLength(16, ErrorMessage = "密码最长16位")]
        [MinLength(6, ErrorMessage = "密码至少6位")]
        public string Password { get; set; }

        [Display(Name = "邮箱")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "组员")]
        public string CoMembers { get; set; }

        public virtual Game Game { get; set; }

        public Leader() { }

        /// <summary>
        /// Will ignore the Id of view model
        /// </summary>
        /// <param name="leader"></param>
        public Leader(LeaderCEViewModel leader)
        {
            StudentNumber = leader.StudentNumber;
            Name = leader.Name;
            Year = leader.Year;
        }

        public void Update(LeaderCEViewModel ceViewModel)
        {
            StudentNumber = ceViewModel.StudentNumber;
            Name = ceViewModel.Name;
            Year = ceViewModel.Year;
        }
    }
}
