using System.Collections.Generic;
using LibraryServices.Abstract;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using LibraryServices.ServiceModels;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LibraryServices.Concrete
{
    public class LibraryPatronService : ILibraryPatron
    {
        private readonly IConfiguration _configuration;
        public LibraryPatronService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async void AddAsync(PatronModel patron)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var json = JsonConvert.SerializeObject(patron);
                StringContent patronContent = new StringContent(JsonConvert.SerializeObject(patron), Encoding.UTF8, "application/json");
                await client.PostAsync($"api/patrons", patronContent);
            }
        }

        public async Task<PatronModel> GetAsync(int patronId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync ($"api/patrons/{patronId}");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                PatronModel patron = JsonConvert.DeserializeObject<PatronModel>(jsonData);
                return patron;
            }
        }

        public async Task<IEnumerable<PatronModel>> GetAllAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/patrons");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                List<PatronModel> patrons = JsonConvert.DeserializeObject<List<PatronModel>>(jsonData);
                return patrons.ToList();
            }
        }

        public async Task<IEnumerable<CheckoutHistoryModel>> GetCheckoutHistoryAsync(int patronId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/patrons/checkouthistory/{patronId}");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                List<CheckoutHistoryModel> checkoutHistory = JsonConvert.DeserializeObject<List<CheckoutHistoryModel>>(jsonData);
                return checkoutHistory.ToList();
            }
        }

        public async Task<IEnumerable<CheckoutModel>> GetCheckoutsAsync(int patronId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/patrons/checkouts/{patronId}");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                List<CheckoutModel> checkouts = JsonConvert.DeserializeObject<List<CheckoutModel>>(jsonData);
                return checkouts.ToList();
            }
        }

        public async Task<IEnumerable<HoldModel>> GetHoldsAsync(int patronId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/patrons/holds/{patronId}");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                List<HoldModel> holds = JsonConvert.DeserializeObject<List<HoldModel>>(jsonData);
                return holds.ToList();
            }
        }
    }
}
