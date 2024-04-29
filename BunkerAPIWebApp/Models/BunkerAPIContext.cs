using Microsoft.EntityFrameworkCore;

namespace BunkerAPIWebApp.Models;

public class BunkerAPIContext : DbContext
{
    public BunkerAPIContext(DbContextOptions<BunkerAPIContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<AdditionalInformation> AdditionalInformations { get; set; }
    public DbSet<Bunker> Bunkers { get; set; }
    public DbSet<BunkerInventory> BunkerInventories { get; set; }
    public DbSet<BunkerSize> BunkerSizes { get; set; }
    public DbSet<Catastrophe> Catastrophes { get; set; }
    public DbSet<FoodQuantity> FoodQuantities { get; set; }
    public DbSet<GameSetting> GameSettings { get; set; }
    public DbSet<GameSettingHumanCard> GameSettingHumanCards { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<GenderType> GenderTypes { get; set; }
    public DbSet<Health> Healths { get; set; }
    public DbSet<Hobby> Hobbies { get; set; }
    public DbSet<HumanCard> HumanCards { get; set; }
    public DbSet<HumanTrait> HumanTraits { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Phobia> Phobias { get; set; }
    public DbSet<Profession> Professions { get; set; }
    public DbSet<ResidenceTime> ResidenceTimes { get; set; }
    public DbSet<SpecialFeature> SpecialFeatures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameSettingHumanCard>()
            .HasKey(gs => new { gs.GameSettingId, gs.HumanCardId });
    }
}

