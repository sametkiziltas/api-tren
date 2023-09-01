using Microsoft.EntityFrameworkCore;
using tren.api.Data.Entities;

namespace tren.api.Api.Data;


public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseInMemoryDatabase(databaseName: "WordDb");
    }
    
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
    
    public DbSet<Word> Words { get; set; }
    
    // public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    // {
    //     var entities = ChangeTracker.Entries().Where(p =>
    //         p.Entity is ModifiableEntity && (p.State == EntityState.Added || p.State == EntityState.Modified));
    //     
    //     foreach (var entity in entities)
    //     {
    //         if (entity.State == EntityState.Added)
    //         {
    //             ((ModifiableEntity)entity.Entity).CreatedOn = DateTime.Now;
    //         }
    //
    //         if (entity.State == EntityState.Modified)
    //         {
    //             ((ModifiableEntity)entity.Entity).UpdatedOn = DateTime.Now;
    //         }
    //     }
    //     
    //     return base.SaveChangesAsync(cancellationToken);
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}