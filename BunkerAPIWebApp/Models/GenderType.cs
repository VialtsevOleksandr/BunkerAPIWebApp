using System.ComponentModel.DataAnnotations;
namespace BunkerAPIWebApp.Models;

public class GenderType
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Стать")]
    public string GenderName { get; set; } = null!;
    //public virtual Gender Gender { get; set; }
}
