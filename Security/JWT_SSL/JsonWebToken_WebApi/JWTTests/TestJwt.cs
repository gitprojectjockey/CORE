using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JWTTests
{
    [TestClass]
    public class TestJwt
    {
        private TestServer _server;
        private HttpClient _client;


        [TestInitialize]
        public void InitTests()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetFullPath(@"../../.."))
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            _server = new TestServer(new WebHostBuilder()
                .UseStartup<JsonWebToken_WebApi.Startup>()
                .UseConfiguration(configuration));

            _client = _server.CreateClient();
        }


        [TestMethod]
        public async Task UnAuthorizedAccessAsync()
        {
            var response = await _client.GetAsync("/api/books");
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public async Task GetToken()
        {
            var requestBody = @"{username: ""enordin"",password: ""Wr400fg!""}";
            var response = await _client.PostAsync("/api/token", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var jsonResponseString = await response.Content.ReadAsStringAsync();
            var jsonTokenObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponseString);
            Assert.IsNotNull(jsonTokenObject);
        }

        [TestMethod]
        public async Task GetBooksWithoutAreRestriction()
        {
            var requestBody = @"{username: ""enordin"",password: ""Wr400fg!""}";
            var response = await _client.PostAsync("/api/token", new StringContent(requestBody, Encoding.UTF8, "application/json"));
            var jsonResponseString = await response.Content.ReadAsStringAsync();
            var jsonTokenObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponseString);

            var token = ((Newtonsoft.Json.Linq.JObject)jsonTokenObject)["token"].ToString();

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/books");
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var booksResponse = await _client.SendAsync(requestMessage);

            Assert.AreEqual(HttpStatusCode.OK, booksResponse.StatusCode);

            var booksResponseString = await booksResponse.Content.ReadAsStringAsync();
            var booksResponseJson =Newtonsoft.Json.Linq.JArray.Parse(booksResponseString);
            Assert.AreEqual(true, booksResponseJson.Count == 4);

        }

    }
}
