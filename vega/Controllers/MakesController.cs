using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Models;
using vega.Persistence;

namespace vega.Controllers {
    public class MakesController : Controller {
        private readonly IMapper mapper;
        public MakesController (VegaDbContext context, IMapper mapper) {
            this.mapper = mapper;
            Context = context;
        }

        public VegaDbContext Context { get; }

        [HttpGet ("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes () {
            var makes = await Context.Makes.Include (m => m.Models).ToListAsync ();
            return mapper.Map<List<Make>, List<MakeResource>> (makes);
        }
    }
}