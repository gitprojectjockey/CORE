using JWT_SSL_WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JWT_SSL_WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly JWTSettings _settings;
        private readonly HttpClient _httpClient;
        public HomeController(IOptions<JWTSettings> settingsOptions, HttpClient httpClient)
        {
            _settings = settingsOptions.Value;
            _httpClient = httpClient;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetBooksAsync()
        {
            var requestBody = @"{username: ""enordin"",password: ""Wr400fg!""}";
            var response = await _httpClient.PostAsync("http://localhost:49928/api/token", new StringContent(requestBody, Encoding.UTF8, "application/json"));
            var jsonResponseString = await response.Content.ReadAsStringAsync();
            var jsonTokenObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponseString);
            return Json(jsonTokenObject);
        }
    }
}
