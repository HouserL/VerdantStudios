using Microsoft.EntityFrameworkCore;
using VerdantAPI.Library.Models;

namespace VerdantAPI.Library.Context;

public class MonsterDBContext : DbContext
{
    public MonsterDBContext(DbContextOptions<MonsterDBContext> options) : base(options) { }

    //public DbSet<Monster> Monsters { get; set; }
    //public DbSet<StandardAction> StandardActions { get; set; }
    //public DbSet<Stats> Stats { get; set; }
    //public DbSet<DamageType> DamageTypes { get; set; }

    //protected override void OnModelCreating(ModelBuilder model)
    //{
    //    model.Entity<Stats>()
    //        .HasNoKey();
    //}
}
