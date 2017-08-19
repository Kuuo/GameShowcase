using System.ComponentModel.DataAnnotations;

namespace GameShowcase.Models
{
    public partial class Screenshot
    {
        public const string Seperator = "\r\n";

        public int Id { get; set; }
        public int GameId { get; set; }

        [Display(Name = "链接")]
        [DataType(DataType.ImageUrl)]
        public string Url { get; set; }

        public virtual Game Game { get; set; }
    }
}
