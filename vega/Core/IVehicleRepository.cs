using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Models;

namespace vega.Core {
    public interface IVehicleRepository {
        Task<Vehicle> GetVehicleAsync (int id);
        Task<IEnumerable<Vehicle>> GetVehiclesAsync ();
        void Add (Vehicle vehicle);
       // Task UpdateAsync (Vehicle vehicle);
        Task<bool> DeleteVehicle (int id);
    }
}