using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;

namespace BunkerAPIWebApp.Models;

public class Profession
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Професія")]
    public string ProfessionName { get; set; } = null!;

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Досвід роботи")]
    public byte WorkExperience { get; set; }
    //public virtual HumanCard HumanCard { get; set; }
}
