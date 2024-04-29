using System.ComponentModel.DataAnnotations;
namespace BunkerAPIWebApp.Models;

public class Hobby
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Хобі")]
    public string HobbyName { get; set; } = null!;
    //public virtual HumanCard HumanCard { get; set; }
}
