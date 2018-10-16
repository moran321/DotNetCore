using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Models;
using vega.Persistence;

namespace vega.Controllers {
    public class FeaturesController {
        private readonly IMapper mapper;
        public FeaturesController (VegaDbContext context, IMapper mapper) {
            Context = context;
            this.mapper = mapper;
        }

        public VegaDbContext Context { get; }

        [HttpGet ("api/features")]
        public async Task<IEnumerable<FeatureResource>> GetFeatures () {
            var features = await Context.Features.ToListAsync ();
            return mapper.Map<List<Feature>, List<FeatureResource>> (features);

        }
    }
}