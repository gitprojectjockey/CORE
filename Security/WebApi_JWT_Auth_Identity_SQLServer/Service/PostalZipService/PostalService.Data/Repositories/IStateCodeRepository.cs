using PostalService.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostalService.Data.Repositories
{
    public interface IStateCodeRepository 
    {
        Task<State> GetStateAsync(string stateAbbrev);
        Task<IEnumerable<State>> GetStatesAsync();
        Task<IEnumerable<State>> GetByUsRegionAsync(string unitedStatesRegion);
        Task UpdateStateCodeBatch(Dictionary<int, string> stateCodeBatch);
    }
}
