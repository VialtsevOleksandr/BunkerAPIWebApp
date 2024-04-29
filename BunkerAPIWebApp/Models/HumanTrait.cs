using System.ComponentModel.DataAnnotations;

namespace BunkerAPIWebApp.Models;

public class HumanTrait
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Риса характеру")]
    public string TraitName { get; set; } = null!;
    //public virtual HumanCard HumanCard { get; set; }
}
