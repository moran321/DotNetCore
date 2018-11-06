using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using vega.Models;

namespace vega.Controllers.Resources {

    public class VehicleResource {
        public int Id { get; set; }
        //public ModelResource Model { get; set; }
        public KeyValPairResource Model { get; set; }
        public KeyValPairResource Make { get; set; }
        public bool IsRegistered { get; set; }
        public ICollection<KeyValPairResource> Features { get; set; }

        public ContactResource Contact { get; set; }
        public DateTime LastUpdate { get; set; }

        public VehicleResource () {
            Features = new Collection<KeyValPairResource> ();
        }
    }
}