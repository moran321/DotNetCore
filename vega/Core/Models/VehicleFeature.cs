using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Models
{
     [Table ("VehicleFeatures")]
    public class VehicleFeature
    {
        public int VehicleId { get; set; } //foreign key
        public int FeatureId { get; set; } //foreign key
        public Feature Feature { get; set; } //navigation property
        public Vehicle Vehicle { get; set; } //navigation property
    }
}