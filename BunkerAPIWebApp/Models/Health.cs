using System.ComponentModel.DataAnnotations;
namespace BunkerAPIWebApp.Models;

public class Health
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Статус здоров'я")]
    public string HealthStatus { get; set; } = null!;

    [Display(Name = "Стадія захворювання")]
    public string? DiseaseStage { get; set; }
    //public virtual HumanCard HumanCard { get; set; }
}
