namespace BunkerAPIWebApp.Models;

public class HumanCard
{
    public int Id { get; set; }
    public int AdditionalInformationId { get; set; }
    public int GenderId { get; set; }
    public int HealthId { get; set; }
    public int HobbyId { get; set; }
    public int HumanTraitId { get; set; }
    public int InventoryId { get; set; }
    public int PhobiaId { get; set; }
    public int ProfessionId { get; set; }
    public int SpecialFeatureId { get; set; }

    //public virtual ICollection<AdditionalInformation> AdditionalInformations { get; set; } = new List<AdditionalInformation>();
    //public virtual ICollection<Gender> Genders { get; set; } = new List<Gender>();
    //public virtual ICollection<Health> Healths { get; set; } = new List<Health>();
    //public virtual ICollection<Hobby> Hobbies { get; set; } = new List<Hobby>();
    //public virtual ICollection<HumanTrait> HumanTraits { get; set; } = new List<HumanTrait>();
    //public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    //public virtual ICollection<Phobia> Phobias { get; set; } = new List<Phobia>();
    //public virtual ICollection<Profession> Professions { get; set; } = new List<Profession>();
    //public virtual ICollection<SpecialFeature> SpecialFeatures { get; set; } = new List<SpecialFeature>();
    public virtual AdditionalInformation AdditionalInformation { get; set; }
    public virtual Gender Gender { get; set; }
    public virtual Health Health { get; set; }
    public virtual Hobby Hobby { get; set; }
    public virtual HumanTrait HumanTrait { get; set; }
    public virtual Inventory Inventory { get; set; }
    public virtual Phobia Phobia { get; set; }
    public virtual Profession Profession { get; set; }
    public virtual SpecialFeature SpecialFeature { get; set; }
    public virtual ICollection<GameSettingHumanCard> GameSettingHumanCards { get; set; } = new List<GameSettingHumanCard>();
}
