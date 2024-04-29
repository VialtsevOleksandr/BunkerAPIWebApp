using System.ComponentModel.DataAnnotations;

namespace BunkerAPIWebApp.Models;

public class ResidenceTime
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Час проживання")]
    public int ResidenceTimeName { get; set; }
    //public virtual Bunker Bunker { get; set; }
}
