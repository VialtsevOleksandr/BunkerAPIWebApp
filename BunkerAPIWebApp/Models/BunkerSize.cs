using System.ComponentModel.DataAnnotations;
namespace BunkerAPIWebApp.Models;

public class BunkerSize
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Розмір бункера")]
    public short Size { get; set; }
    //public virtual Bunker Bunker { get; set; }
}
