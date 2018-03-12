using EWN.Data.Repo.Core;
using PostalService.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostalService.Data.Repositories
{
    public interface IZipCodeRepository : IRepository<ZipCode>
    {
        Task<ZipCode> GetZipCodeAsync(string zipCode);
        Task<IEnumerable<ZipCode>> GetZipCodesAsync();
        Task<IEnumerable<ZipCode>> GetByStateAsync(string stateAbbrev);
        Task<IEnumerable<ZipCode>> GetZipCodesByRangeAsync(ZipCode zip, int range);
        Task UpdateCityBatch(Dictionary<string, string> cityBatch);
        Task UpdateCity(string zip, string city);
        Task<IEnumerable<ZipCode>> GetPagedZipCodes(int pageNumber, int take);
    }
}
