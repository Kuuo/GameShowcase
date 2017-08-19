using GameShowcase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameShowcase.Areas.Manage.Models.ViewModels
{
    public class GameIntroViewModel
    {
        public int Id { get; set; }

        [MaxLength(3000, ErrorMessage = "长度不能超过3000")]
        public string Content { get; set; }

        public GameIntroViewModel() { }

        public GameIntroViewModel(Game game)
        {
            Id = game.Id;
            Content = game.Description;
        }
    }
}
