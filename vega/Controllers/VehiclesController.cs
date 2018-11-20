using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Core;
using vega.Models;
using vega.Persistence;

// API to create/update/delete vehicles (CRUD)

namespace vega.Controllers {
    [Route ("/api/vehicles")]
    public class VehiclesController : Controller {

        private readonly IMapper mapper;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUnitOfWork unitOfWork;


        public VehiclesController (IMapper mapper, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork) {
            this.mapper = mapper;
            this.vehicleRepository = vehicleRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleResource>> GetVehicles () {
            //var vehicles = await context.Vehicles.ToListAsync ();
            var vehicles = await vehicleRepository.GetVehiclesAsync ();
            return mapper.Map<List<Vehicle>, List<VehicleResource>> (vehicles.ToList ());
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle ([FromBody] SaveVehicleResource vehicleResource) {

            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle> (vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            // this.context.Vehicles.Add (vehicle);
            vehicleRepository.Add (vehicle);
            await unitOfWork.CompleteAsync ();

            vehicle = await vehicleRepository.GetVehicleAsync (vehicle.Id);
            var resultObj = mapper.Map<Vehicle, VehicleResource> (vehicle);
            return Ok (resultObj);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> UpdateVehicle (int id, [FromBody] SaveVehicleResource vehicleResource) {

            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }

            // var vehicle = await this.Context.Vehicles.Include (v => v.Features).SingleOrDefaultAsync (v => v.Id == id);
            var vehicle = await vehicleRepository.GetVehicleAsync (id);

            if (vehicle == null) {
                return NotFound ();
            }
            mapper.Map<SaveVehicleResource, Vehicle> (vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            // await this.context.SaveChangesAsync ();
            await unitOfWork.CompleteAsync ();

            var resultObj = mapper.Map<Vehicle, VehicleResource> (vehicle);
            return Ok (resultObj);
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteVehicle (int id) {
            //    var vehicle = await this.context.Vehicles.FindAsync (id);
            //  var vehicle = await vehicleRepository.GetVehicleAsync(id);
            // if (vehicle == null) {
            //     return NotFound ();
            // }
            // context.Remove (vehicle);
            // vehicleRepository.DeleteVehicle(vehicle);
            await vehicleRepository.DeleteVehicle (id);
            //  await this.context.SaveChangesAsync ();
            await unitOfWork.CompleteAsync ();
            return Ok (id);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetVehicle (int id) {
            var vehicle = await vehicleRepository.GetVehicleAsync (id);
            if (vehicle == null) {
                return NotFound ();
            }
            var vr = mapper.Map<Vehicle, VehicleResource> (vehicle);
            return Ok (vr);
        }
    }
}