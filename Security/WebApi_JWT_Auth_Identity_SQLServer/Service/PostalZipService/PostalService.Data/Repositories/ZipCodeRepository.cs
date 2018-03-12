using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EWN.Data.Repo.Core;
using Microsoft.EntityFrameworkCore;
using PostalService.Data.Entities;

namespace PostalService.Data.Repositories
{
    public class ZipCodeRepository : Repository<ZipCode>, IZipCodeRepository
    {
        private readonly GeoDataDbContext _context;

        public ZipCodeRepository(GeoDataDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ZipCode>> GetPagedZipCodes(int pageNumber = 1, int take = 20)
        {
            return await _context.ZipCode
                .Include(zc => zc.State)
                .OrderBy(zc => zc.State.Name)
                .ThenBy(zc => zc.City)
                .Skip(take * (pageNumber-1))
                .Take(take).AsQueryable().ToListAsync();
        }

        public async Task<ZipCode> GetZipCodeAsync(string zipCode)
        {
            return await base.SingleOrDefaultAsync(zc => zc.Zip == zipCode);
        }
        public async Task<IEnumerable<ZipCode>> GetZipCodesAsync()
        {
            return await base.GetAsync(null,zc => zc.OrderBy(o => o.State.Name));
        }

        public async Task<IEnumerable<ZipCode>> GetByStateAsync(string stateAbbrev)
        {
            return await base.GetAsync(zc => zc.State.Abbreviation == stateAbbrev, zc => zc.OrderBy(o => o.State.Abbreviation), "State");
        }

        public async Task<IEnumerable<ZipCode>> GetZipCodesByRangeAsync(ZipCode zip, int range)
        {
            double degrees = range / 69.047;

            return await _context.ZipCode
                .Include(zc => zc.State)
                .Where(zc => (zc.Latitude <= zip.Latitude + degrees && zc.Latitude >= zip.Latitude - degrees) &&
                            (zc.Longitude <= zip.Longitude + degrees && zc.Longitude >= zip.Longitude - degrees))
                .ToListAsync();
        }

        public async Task UpdateCity(string zip, string city)
        {
            //Unit of work is responsible for the save
            ZipCode zipCode = await _context.ZipCode.FirstOrDefaultAsync(zc => zc.Zip == zip);
            if (zipCode != null)
            {
                zipCode.City = city;
            }
        }

        public async Task UpdateCityBatch(Dictionary<string, string> cityBatch)
        {
            //Unit of work is responsible for the save
            List<string> cityBatchList = (from kvp in cityBatch select kvp.Key).ToList(); // Linq-to-Entities
            List<ZipCode> zips = await _context.ZipCode.Where(e => cityBatchList.Contains(e.Zip)).ToListAsync();
            zips.ForEach(e => e.City = cityBatch[e.Zip]);
        }
    }
}
