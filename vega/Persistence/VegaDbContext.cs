using Microsoft.EntityFrameworkCore;
using vega.Models;

namespace vega.Persistence {
    public class VegaDbContext : DbContext {

        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public VegaDbContext (DbContextOptions<VegaDbContext> options) : base (options) {

        }


        //verify that the key of 'VehicleFeature' has 2 properties
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<VehicleFeature>().HasKey( vf => new {vf.VehicleId, vf.FeatureId});
        }
    }
}