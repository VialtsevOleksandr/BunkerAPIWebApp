using System.ComponentModel.DataAnnotations;
namespace BunkerAPIWebApp.Models;

public class Catastrophe
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Катастрофа")]
    public string CatastropheName { get; set; } = null!;
    //public virtual GameSetting GameSetting { get; set; }
}
