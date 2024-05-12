using System.ComponentModel.DataAnnotations;
namespace BunkerAPIWebApp.Models;

public class Bunker
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Розмір бункера")]
    public int BunkerSizeId { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Час проживання")]
    public int ResidenceTimeId { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Запас їжі")]
    public int FoodQuantityId { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Наявний у бункері інвентар")]
    public int BunkerInventoryId { get; set; }

    //public virtual ICollection<BunkerSize> BunkerSizes { get; set; } = new List<BunkerSize>();
    //public virtual ICollection<ResidenceTime> ResidenceTimes { get; set; } = new List<ResidenceTime>();
    //public virtual ICollection<FoodQuantity> FoodQuantities { get; set; } = new List<FoodQuantity>();
    //public virtual ICollection<BunkerInventory> BunkerInventories { get; set; } = new List<BunkerInventory>();
    public virtual BunkerSize? BunkerSize { get; set; }
    public virtual ResidenceTime? ResidenceTime { get; set; }
    public virtual FoodQuantity? FoodQuantity { get; set; }
    public virtual BunkerInventory? BunkerInventory { get; set; }
    
    //public virtual GameSetting GameSetting { get; set; }
}
