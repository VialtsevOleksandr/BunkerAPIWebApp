using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BunkerAPIWebApp.Models;

public class GameSettingHumanCard
{
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "ID налаштувань гри")]
    public int GameSettingId { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "ID картки людини")]
    public int HumanCardId { get; set; }
    public virtual GameSetting? GameSetting { get; set; }
    public virtual HumanCard? HumanCard { get; set; }
}
