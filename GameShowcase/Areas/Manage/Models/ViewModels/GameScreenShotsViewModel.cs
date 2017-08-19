using GameShowcase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShowcase.Areas.Manage.Models.ViewModels
{
    public class GameScreenShotsViewModel
    {
        public string Urls { get; set; }

        public GameScreenShotsViewModel() { }

        public GameScreenShotsViewModel(Game game)
        {
            if (game.Screenshots == null) return;

            Urls = string.Join(Screenshot.Seperator, game.Screenshots.Select(ss => ss.Url)).Trim();
        }

        public IEnumerable<Screenshot> Parse()
        {
            if (string.IsNullOrEmpty(Urls)) yield break;

            Urls = Urls.Trim();
            var urls = Urls.Split(
                new string[] { Screenshot.Seperator }, 
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var url in urls)
            {
                yield return new Screenshot { Url = url };
            }
        }
    }
}
