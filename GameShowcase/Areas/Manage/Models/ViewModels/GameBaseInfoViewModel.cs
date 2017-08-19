using GameShowcase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameShowcase.Areas.Manage.Models.ViewModels
{
    public class GameBaseInfoViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "游戏标题")]
        public string Title { get; set; }

        [Display(Name = "游戏种类")]
        public string Genre { get; set; }

        [Display(Name = "封面链接")]
        [DataType(DataType.ImageUrl)]
        public string CoverUrl { get; set; }

        [Display(Name = "演示视频链接")]
        [DataType(DataType.Url)]
        public string DemoUrl { get; set; }

        public GameBaseInfoViewModel() { }

        public GameBaseInfoViewModel(Game game)
        {
            Id = game.Id;
            Title = game.Title;
            Genre = game.Genre;
            CoverUrl = game.CoverUrl;
            DemoUrl = game.DemoUrl;
        }
    }
}
