using LibraryServices.Abstract;
using LibraryServices.ServiceModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LibraryServices.Concrete
{
    public class LibraryBranchService : ILibraryBranch
    {
        private readonly IConfiguration _configuration;
        public LibraryBranchService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<LibraryBranchDetailModel>> GetAllAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/branches");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                IEnumerable<LibraryBranchDetailModel> branches = JsonConvert.DeserializeObject<IEnumerable<LibraryBranchDetailModel>>(jsonData);
                return branches;
            }
        }
        
        public async Task<LibraryBranchModel> GetByIdAsync(int branchId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/branches/{branchId}");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                LibraryBranchModel branch = JsonConvert.DeserializeObject<LibraryBranchModel>(jsonData);
                return branch;
            }
        }

        public decimal GetLibraryAssetsValue(IEnumerable<LibraryAssetModel> LibraryAssets)
        {
            var assetsValue = LibraryAssets.Select(a => a.Cost);
            return assetsValue.Sum();
        }

        //public async Task<IEnumerable<string>> GetLibraryBranchHours(int branchId)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
        //        MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
        //        client.DefaultRequestHeaders.Accept.Add(contentType);
        //        HttpResponseMessage response = await client.GetAsync($"api/branches/{branchId}/hours");
        //        string jsonData = response.Content.ReadAsStringAsync().Result;
        //        IEnumerable<string> branchHours = JsonConvert.DeserializeObject<IEnumerable<string>>(jsonData);
        //        return branchHours;
        //    }
        //}
    }
}

