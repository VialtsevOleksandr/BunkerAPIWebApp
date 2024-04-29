using System.ComponentModel.DataAnnotations;

namespace BunkerAPIWebApp.Models;

public class Inventory
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Інвентар")]
    public string InventoryName { get; set; } = null!;
    //public virtual HumanCard HumanCard { get; set; }
}
