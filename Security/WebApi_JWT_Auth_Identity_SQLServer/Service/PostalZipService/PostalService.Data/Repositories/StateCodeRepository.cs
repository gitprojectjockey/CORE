using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EWN.Data.Repo.Core;
using Microsoft.EntityFrameworkCore;
using PostalService.Data.Entities;

namespace PostalService.Data.Repositories
{
    public class StateCodeRepository : Repository<State>, IStateCodeRepository
    {
        private readonly GeoDataDbContext _context;
        public StateCodeRepository(GeoDataDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<State>> GetStatesAsync()
        {
            return await base.GetAsync(null, s => s.OrderBy(o => o.Abbreviation));
        }

        public async Task<State> GetStateAsync(string stateAbbrev)
        {
            return await base.SingleOrDefaultAsync(s => s.Abbreviation == stateAbbrev);
        }

        public async Task<IEnumerable<State>> GetByUsRegionAsync(string unitedStatesRegion)
        {
            return await base.GetAsync(s => s.USRegion == unitedStatesRegion, s => s.OrderBy(o => o.Name));
        }

        public async Task UpdateStateCodeBatch(Dictionary<int, string> stateCodeBatch)
        {
            //Unit of work is responsible for the save
            int[] stateIdList = stateCodeBatch.Select(sc => sc.Key).ToArray();

            List<State> states = await _context.State
                .Where(s => stateIdList.Contains(s.StateId)).ToListAsync();

            states.ForEach(s => s.SSCode = stateCodeBatch[s.StateId]);
        }
    }
}
