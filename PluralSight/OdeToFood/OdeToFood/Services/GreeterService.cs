using Microsoft.Extensions.Configuration;

namespace OdeToFood.Services
{
    public interface IGreeterService
    {
        string GetGreeting();
    }

    public class GreeterService : IGreeterService
    {
        private readonly string _greeting;

        public GreeterService(IConfiguration configuration)
        {
            _greeting = configuration["greeting"];
        }

        public string GetGreeting()
        {
            return _greeting;
        }
    }
}
