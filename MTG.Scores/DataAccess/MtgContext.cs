using MTG.Scores.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MTG.Scores.DataAccess
{
  public class MtgContext : DbContext
  {
    public MtgContext() : base("MtgDbConnectionString")
    {
    }

    public DbSet<Match> Matches { get; set; }

    public DbSet<Player> Players { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
      modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
    }
  }
}