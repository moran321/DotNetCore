using System.Collections.Generic;
using System.Collections.ObjectModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace vega.Controllers.Resources {
    public class MakeResource: KeyValPairResource {
    
        public ICollection<KeyValPairResource> Models { get; set; }

        public MakeResource () {
            Models = new Collection<KeyValPairResource> ();
        }

    }
}