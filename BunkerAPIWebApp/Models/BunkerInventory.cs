using System.ComponentModel.DataAnnotations;
namespace BunkerAPIWebApp.Models;

public class BunkerInventory
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Наявний у бункері інвентар")]
    public string BunkerInventoryName { get; set; } = null!;
    //public virtual Bunker Bunker { get; set; }
}
