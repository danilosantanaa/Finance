using FinanceWasm.Models;
using System.Net.Http.Json;

namespace FinanceWasm.Services {
    public class CobrancaService {
        private readonly HttpClientService _httpClient;

        public CobrancaService(HttpClientService httpClient)
        {
            _httpClient = httpClient;
        }

        #region GET
        public async Task<List<Cobranca>> GetAll() =>
            await _httpClient.GetFromJsonAsync<List<Cobranca>>("./api/cobranca");

        public async Task<Cobranca> GetById(int id) =>
            await _httpClient.GetFromJsonAsync<Cobranca>($"./api/cobranca/{id}");

        public async Task<HttpResponseMessage> GetChangeStatus(int id, string status) =>
            await _httpClient.GetAsync($"./api/cobranca/{id}/changeStatus/{status}");
        #endregion

        #region POST
        public async Task<HttpResponseMessage> Post(Cobranca cobranca) =>
            await _httpClient.PostAsJsonAsync("./api/cobranca", cobranca);
        #endregion

        #region PUT
        public async Task<HttpResponseMessage> Put(Cobranca cobranca, int id) =>
            await _httpClient.PutAsJsonAsync($"./api/cobranca/{id}", cobranca);
        #endregion
    }
}
