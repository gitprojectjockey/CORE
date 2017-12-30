using LibraryServices.Abstract;
using System;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using LibraryServices.ServiceModels;

namespace LibraryServices.Concrete
{
    public class LibraryCheckoutService : ILibraryCheckout
    {
        private readonly IConfiguration _configuration;
        public LibraryCheckoutService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<LibraryAssetCheckoutModel> GetByIdAsync(int assetId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/catalog/checkouts/get/{assetId}");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                LibraryAssetCheckoutModel checkout = JsonConvert.DeserializeObject<LibraryAssetCheckoutModel>(jsonData);
                return checkout;
            }
        }

        public async Task<LibraryAssetCheckoutModel> GetHoldTypeCheckoutAsync(int assetId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/catalog/holds/get/{assetId}");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                LibraryAssetCheckoutModel holdTypeCheckout = JsonConvert.DeserializeObject<LibraryAssetCheckoutModel>(jsonData);
                return holdTypeCheckout;
            }
        }

        public void CheckoutItem(int assetId, int libraryCardId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.PostAsync($"api/catalog/asset/checkout/{assetId}/{libraryCardId}", new HttpRequestMessage().Content).Wait();
            }
        }

        public void CheckInItem(int assetId,int libraryCardId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.PostAsync($"api/catalog/asset/checkin/{assetId}/{libraryCardId}", new HttpRequestMessage().Content).Wait();
            }
        }

        public void MarkItemFound(int assetId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.PostAsync($"api/catalog/asset/markFound/{assetId}", new HttpRequestMessage().Content).Wait();
            }
        }

        public void HoldItem(int assetId, int libraryCardId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.PostAsync($"api/catalog/asset/hold/{assetId}/{libraryCardId}", new HttpRequestMessage().Content).Wait();
            }
        }

        //public Checkout GetLatestCheckout(int id)
        //{
        //    return _context.Checkouts
        //        .Where(c => c.LibraryAsset.Id == id)
        //        .OrderByDescending(c => c.Since)
        //        .FirstOrDefault();
        //}

        //public int GetAvailableCopies(int id)
        //{
        //    var numberOfCopies = GetNumberOfCopies(id);

        //    var numberCheckedOut = _context.Checkouts
        //        .Where(a => a.LibraryAsset.Id == id
        //                 && a.LibraryAsset.Status.Name == "Checked Out")
        //                 .Count();

        //    return numberOfCopies - numberCheckedOut;
        //}

        //public int GetNumberOfCopies(int id)
        //{
        //    return _context.LibraryAssets
        //        .FirstOrDefault(a => a.Id == id)
        //        .NumberOfCopies;
        //}

        //private DateTime GetDefaultCheckoutTime(DateTime now)
        //{
        //    return now.AddDays(30);
        //}



        //public string GetCurrentHoldPatron(int holdId)
        //{

        //    var hold = _context.Holds
        //      .Include(a => a.LibraryAsset)
        //      .Include(a => a.LibraryCard)
        //      .Where(v => v.Id == holdId);

        //    var cardId = hold
        //        .Include(a => a.LibraryCard)
        //        .Select(a => a.LibraryCard.Id)
        //        .FirstOrDefault();

        //    var patron = _context.Patrons
        //        .Include(p => p.LibraryCard)
        //        .FirstOrDefault(p => p.LibraryCard.Id == cardId);

        //    return $"{patron.FirstName}  {patron.LastName}";
        //}

        //public string GetCurrentHoldPlaced(int holdId)
        //{
        //    var hold = _context.Holds
        //    .Include(a => a.LibraryAsset)
        //    .Include(a => a.LibraryCard)
        //    .Where(v => v.Id == holdId);

        //    return hold.Select(a => a.HoldPlaced)
        //        .FirstOrDefault().ToString();
        //}



        //public string GetCurrentCheckoutPatron(int id)
        //{
        //    var checkout = _context.Checkouts
        //        .Include(a => a.LibraryAsset)
        //        .Include(a => a.LibraryCard)
        //        .Where(a => a.LibraryAsset.Id == id)
        //        .FirstOrDefault();

        //    if (checkout == null)
        //    {
        //        return "Not checked out.";
        //    }

        //    var cardId = checkout.LibraryCard.Id;

        //    var patron = _context.Patrons
        //        .Include(p => p.LibraryCard)
        //        .Where(c => c.LibraryCard.Id == cardId)
        //        .FirstOrDefault();

        //    return patron.FirstName + " " + patron.LastName;
        //}
    }
}
