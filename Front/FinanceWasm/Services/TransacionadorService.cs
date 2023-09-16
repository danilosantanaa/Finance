using FinanceWasm.Models;
using System.Net.Http.Json;

namespace FinanceWasm.Services {
    public class TransacionadorService {
        private readonly HttpClientService _httpClient;

        public TransacionadorService(HttpClientService httpClient)
        {
            _httpClient = httpClient;
        }

        #region GET
        public async Task<List<Transacionador>> GetAll() =>
            await _httpClient.GetFromJsonAsync<List<Transacionador>>("api/transacionador");

        public async Task<Transacionador> GetById(int id) =>
            await _httpClient.GetFromJsonAsync<Transacionador>($"api/transacionador/{id}");
        #endregion

        #region POST
        public async Task<HttpResponseMessage> Post(Transacionador transacionador) =>
            await _httpClient.PostAsJsonAsync("api/transacionador", transacionador);
        #endregion

        #region PUT
        public async Task<HttpResponseMessage> Put(Transacionador transacionador, int id) =>
            await _httpClient.PutAsJsonAsync($"api/transacionador/{id}", transacionador);
        #endregion
    }
}
