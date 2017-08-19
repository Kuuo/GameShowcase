using GameShowcase.Areas.Manage.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameShowcase.Models
{
    public partial class Game
    {
        public Game()
        {
            Screenshots = new HashSet<Screenshot>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "此项不可为空")]
        [Display(Name = "游戏标题")]
        public string Title { get; set; }

        [Display(Name = "游戏种类")]
        public string Genre { get; set; }

        [Display(Name = "简介")]
        public string Description { get; set; }

        [Display(Name = "封面链接")]
        [DataType(DataType.ImageUrl)]
        public string CoverUrl { get; set; }

        [Display(Name = "演示视频链接")]
        [DataType(DataType.Url)]
        public string DemoUrl { get; set; }

        public virtual ICollection<Screenshot> Screenshots { get; set; }
        public virtual Leader Leader { get; set; }

        public string Developers => $"{Leader.Name} {Leader.CoMembers}".Trim();
        
        public Game(GameBaseInfoViewModel baseInfoVM)
        {
            Title = baseInfoVM.Title;
            Genre = baseInfoVM.Genre;
            CoverUrl = baseInfoVM.CoverUrl;
            DemoUrl = baseInfoVM.DemoUrl;
        }

        public void Update(GameBaseInfoViewModel baseInfoVM)
        {
            Title = baseInfoVM.Title;
            Genre = baseInfoVM.Genre;
            CoverUrl = baseInfoVM.CoverUrl;
            DemoUrl = baseInfoVM.DemoUrl;
        }
    }
}
