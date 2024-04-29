using System.ComponentModel.DataAnnotations;

namespace BunkerAPIWebApp.Models;

public class SpecialFeature
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Додаткова дія")]
    public string SpecialFeatureName { get; set; } = null!;
    //public virtual HumanCard HumanCard { get; set; }
}
