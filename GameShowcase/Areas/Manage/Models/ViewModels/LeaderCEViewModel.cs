using GameShowcase.Models;
using System.ComponentModel.DataAnnotations;

namespace GameShowcase.Areas.Manage.Models.ViewModels
{
    /// <summary>
    /// Leader view-model for create and edit
    /// </summary>
    public class LeaderCEViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [Display(Name = "学号")]
        [MaxLength(10, ErrorMessage = "学号最长10位")]
        public string StudentNumber { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [Display(Name = "姓名")]
        [MaxLength(10, ErrorMessage = "姓名最长10个字符")]
        public string Name { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [Display(Name = "年份")]
        public int Year { get; set; }

        public LeaderCEViewModel() { }

        public LeaderCEViewModel(Leader leader)
        {
            Id = leader.Id;
            StudentNumber = leader.StudentNumber;
            Name = leader.Name;
            Year = leader.Year;
        }
    }
}
