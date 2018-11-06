using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Models;

namespace vega.Persistence {

    //Repository = A collection of domain objects in memory
    public class VehicleRepository : IVehicleRepository {

        private readonly VegaDbContext context;
        public VehicleRepository (VegaDbContext context) {
            this.context = context;
        }

        public async Task AddAsync (Vehicle vehicle) {
            this.context.Vehicles.Add (vehicle);
         //   await this.context.SaveChangesAsync ();
        }

        // public async Task UpdateAsync (Vehicle vehicle) {
        //     var vehicle = await this.GetVehicleAsync (id);

        //     if (vehicle == null) {
        //         return NotFound ();
        //     }
        //     mapper.Map<SaveVehicleResource, Vehicle> (vehicleResource, vehicle);
        //     vehicle.LastUpdate = DateTime.Now;

        //  //   await this.context.SaveChangesAsync ();

        // }

         public async Task<bool> DeleteVehicle (int id) {
            var vehicle = await this.context.Vehicles.FindAsync (id);
            if (vehicle == null) {
                return false;
            }
            context.Remove (vehicle);
        //    await this.context.SaveChangesAsync ();
            return true;
         }
        public async Task<Vehicle> GetVehicleAsync (int id) {
            var vehicle = await this.context.Vehicles
                .Include (vega => vega.Features)
                .ThenInclude (vf => vf.Feature)
                .Include (v => v.Model)
                .ThenInclude (v => v.Make)
                .SingleOrDefaultAsync (v => v.Id == id);
            return vehicle;
        }
        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync () {
            var vehicles = await context.Vehicles.ToListAsync ();
            return vehicles;
        }
    }
}