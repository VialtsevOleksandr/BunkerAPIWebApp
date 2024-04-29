using System.ComponentModel.DataAnnotations;
namespace BunkerAPIWebApp.Models;

public class FoodQuantity
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Запас їжі")]
    public string FoodQuantityName { get; set; } = null!;
    //public virtual Bunker Bunker { get; set; }
}
