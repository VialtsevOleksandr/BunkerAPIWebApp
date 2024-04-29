using System.ComponentModel.DataAnnotations;
namespace BunkerAPIWebApp.Models;

public class Gender
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Стать")]
    public int GenderTypeId { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Вік")]
    public byte Age { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Чи може мати дитину")]
    public bool IsChildfree { get; set; }
    public virtual GenderType GenderType { get; set; }
    //public virtual HumanCard HumanCard { get; set; }
}
