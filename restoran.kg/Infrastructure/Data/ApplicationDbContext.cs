using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Application.Interface.Common;
using Microsoft.EntityFrameworkCore;
using Domain.Models;


namespace Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Table> Tables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Table>()
            .HasOne(t => t.Restaurant)
            .WithMany(r => r.Tables)
            .HasForeignKey(t => t.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}

