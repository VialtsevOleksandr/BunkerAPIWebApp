using System.ComponentModel.DataAnnotations;
namespace BunkerAPIWebApp.Models;

public class AdditionalInformation
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Додаткова інформація")]
    public string AdditionalInformationName { get; set; } = null!;
    //public virtual HumanCard HumanCard { get; set; }
}
