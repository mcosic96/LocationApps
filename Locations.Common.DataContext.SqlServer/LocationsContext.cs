using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Locations.Shared
{
    public partial class LocationsContext : DbContext
    {
        public LocationsContext()
        {
        }

        public LocationsContext(DbContextOptions<LocationsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SearchLocation> SearchLocations { get; set; } = null!;
        public virtual DbSet<Place> Places { get; set; } = null!;
        public virtual DbSet<Result> Results { get; set; } = null!;
        public virtual DbSet<Geometry> Geometries { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Viewport> Viewports { get; set; } = null!;
        public virtual DbSet<Northeast> Northeasts { get; set; } = null!;
        public virtual DbSet<Southwest> Southwests { get; set; } = null!;
        public virtual DbSet<Opening_Hours> Opening_Hours { get; set; } = null!;
        public virtual DbSet<Plus_Code> Plus_Codes { get; set; } = null!;
        public virtual DbSet<Photo> Photos { get; set; } = null!;


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        string dir = Environment.CurrentDirectory;
        //        string path = string.Empty;

        //        if (dir.EndsWith("net7.0"))
        //        {
        //            // Running in the <project>\bin\<Debug|Release>\net7.0 directory.
        //            path = Path.Combine("..", "..", "..", "..", "Locations.db");
        //        }
        //        else
        //        {
        //            // Running in the <project> directory.
        //            path = Path.Combine("..", "Locations.db");
        //        }

        //        optionsBuilder.UseSqlite($"Filename={path}", b => b.MigrationsAssembly("Locations.Common.EntityModels.Sqlite"));
        //    }
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}