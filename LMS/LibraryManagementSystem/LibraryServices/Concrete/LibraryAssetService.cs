using System.Collections.Generic;
using LibraryData.Models;
using System.Linq;
using LibraryServices.Abstract;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using LibraryServices.ServiceModels;
using System.Text;

namespace LibraryServices.Concrete
{
    public class LibraryAssetService : ILibraryAsset
    {
        private readonly IConfiguration _configuration;
        public LibraryAssetService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async void AddAsync(LibraryAssetModel libraryAsset)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var json = JsonConvert.SerializeObject(libraryAsset);
                StringContent patronContent = new StringContent(JsonConvert.SerializeObject(libraryAsset), Encoding.UTF8, "application/json");
                await client.PostAsync($"api/catalog/asset", patronContent);
            }
        }

        public async Task<IEnumerable<LibraryAssetModel>> GetAllAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/catalog");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                IEnumerable<LibraryAssetModel> libraryAssets = JsonConvert.DeserializeObject<IEnumerable<LibraryAssetModel>>(jsonData);
                return libraryAssets;
            }
        }

        public async Task<LibraryAssetModel> GetByIdAsync(int assetId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/catalog/{assetId}");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                LibraryAssetModel libraryAsset = JsonConvert.DeserializeObject<LibraryAssetModel>(jsonData);
                return libraryAsset;
            }
        }

        public async Task<LibraryAssetDetailModel> GetAssetDetailsAsync(int assetId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/catalog/detail/{assetId}");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                LibraryAssetDetailModel libraryAssetDetails = JsonConvert.DeserializeObject<LibraryAssetDetailModel>(jsonData);
                return libraryAssetDetails;
            }
        }

        public async Task<LibraryBranchModel> GetCurrentLocationAsync(int assetId)
        {
            LibraryAssetModel libraryAsset = await GetByIdAsync(assetId);
            return libraryAsset.Location;
        }

        public async Task<string> GetAssetLocationNameAsync(int assetId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/catalog/get/{assetId}/currentBranchLocation");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                LibraryBranchModel branch = JsonConvert.DeserializeObject<LibraryBranchModel>(jsonData);

                return branch != null ? branch.Name : "Unknown";
            }
        }

        public async Task<IEnumerable<BookModel>> GetBookAssetsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/catalog/books/get");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                IEnumerable<BookModel> libraryBookAssets = JsonConvert.DeserializeObject<IEnumerable<BookModel>>(jsonData);
                return libraryBookAssets;
            }
        }

        public async Task<IEnumerable<VideoModel>> GetVideoAssetsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/catalog/videos/get");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                IEnumerable<VideoModel> libraryVideoAssets = JsonConvert.DeserializeObject<IEnumerable<VideoModel>>(jsonData);
                return libraryVideoAssets;
            }
        }

        public async Task<string> GetDeweyIndexAsync(int bookId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/catalog/books/get/{bookId}/deweyIndex");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                string deweyIndex = JsonConvert.DeserializeObject<string>(jsonData);

                return deweyIndex ?? "Unknown";
            }
        }

        public async Task<string> GetIsbnAsync(int bookId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/catalog/books/get/{bookId}/isbn");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                string isbn = JsonConvert.DeserializeObject<string>(jsonData);

                return isbn ?? "Unknown";
            }
        }

        public async Task<string> GetAssetTitleAsync(int assetId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/catalog/get/{assetId}/title");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                string title = JsonConvert.DeserializeObject<string>(jsonData);

                return title ?? "Unknown";
            }
        }

        public async Task<string> GetAssetTypeAsync(int assetId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/catalog/get/{assetId}/type");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                string type = JsonConvert.DeserializeObject<string>(jsonData);

                return type ?? "Unknown";
            }
        }

        public async Task<string> GetAuthorOrDirectorAsync(int assetId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/catalog/get/{assetId}/authurOrDirectory");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                string name = JsonConvert.DeserializeObject<string>(jsonData);

                return name ?? "Unknown";
            }
        }

        public async Task<LibraryCardModel> GetLibraryCardByAssetIdAsync(int assetId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["LMSServices:BaseAddress"]);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = await client.GetAsync($"api/catalog/get/{assetId}/libraryCard");
                string jsonData = response.Content.ReadAsStringAsync().Result;
                LibraryCardModel card = JsonConvert.DeserializeObject<LibraryCardModel>(jsonData);

                return card;
            }
        }
    }
}

