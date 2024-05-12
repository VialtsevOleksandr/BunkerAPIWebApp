using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace BunkerAPIWebApp.Models;

public class GameSetting
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Кількість гравців")]
    public int CountOfPlayers { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Чи буде гра з друзями?")]
    public bool IsGameWithFriends { get; set; }

    //[Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Катастрофа")]
    public int CatastropheId { get; set; }

    //[Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Бункер")]
    public int BunkerId { get; set; }

    //public virtual ICollection<Catastrophe> Catastrophes { get; set; } = new List<Catastrophe>();
    //public virtual ICollection<Bunker> Bunkers { get; set; } = new List<Bunker>();
    public virtual Catastrophe? Catastrophe { get; set; }
    public virtual Bunker? Bunker { get; set; }
    public virtual ICollection<GameSettingHumanCard> GameSettingHumanCards { get; set; } = new List<GameSettingHumanCard>();

}
