using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Models;

namespace vega.Persistence {
    public interface IVehicleRepository {
        Task<Vehicle> GetVehicleAsync (int id);
        Task<IEnumerable<Vehicle>> GetVehiclesAsync ();
        Task AddAsync (Vehicle vehicle);
       // Task UpdateAsync (Vehicle vehicle);
        Task<bool> DeleteVehicle (int id);
    }
}