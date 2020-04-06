using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBoard.Models;

namespace Board.Models
{
  public class AppDbContext : IdentityDbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Advert> Adverts { get; set; }
    public DbSet<AdvertPhoto> AdvertPhotos { get; set; }
    public DbSet<Citi> Citi { get; set; }
    public DbSet<State> States { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<AdvertPhoto>()
          .HasOne(p => p.Advert)
          .WithMany(t => t.AdvertPhotos)
          .OnDelete(DeleteBehavior.Cascade);

    }

  }
}
