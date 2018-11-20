using System.Threading.Tasks;
using vega.Core;

namespace vega.Persistence {
    public class UnitOfWork : IUnitOfWork {
        public UnitOfWork (VegaDbContext context) {
            Context = context;
        }

        private readonly VegaDbContext Context;

        public async Task CompleteAsync () {
            await Context.SaveChangesAsync ();
        }
    }
}