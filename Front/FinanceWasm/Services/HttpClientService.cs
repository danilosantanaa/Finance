using System.Net.Http.Headers;

namespace FinanceWasm.Services
{
    public class HttpClientService : HttpClient
    {
        public HttpClientService(IConfiguration configuration)
        {
            var token = configuration["ApiConfig:TokenJwtTest"];
            BaseAddress = new Uri(configuration["ApiConfig:Host"]);
        }
    }
}