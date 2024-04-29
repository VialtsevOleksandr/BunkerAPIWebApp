using System.ComponentModel.DataAnnotations;

namespace BunkerAPIWebApp.Models;

public class Phobia
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Фобія")]
    public string PhobiaName { get; set; } = null!;
    //public virtual HumanCard HumanCard { get; set; }
}
