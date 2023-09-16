using FinanceWasm.Models;
using System.Net.Http.Json;

namespace FinanceWasm.Services {
    public class CategoriaService {
        private readonly HttpClientService _httpClient;

        public CategoriaService(HttpClientService httpClient)
        {
            _httpClient = httpClient;
        }

        #region GET
        public async Task<List<Categoria>> GetAll() =>
            await _httpClient.GetFromJsonAsync<List<Categoria>>("./api/categoria");

        public async Task<Categoria> GetById(int id) =>
            await _httpClient.GetFromJsonAsync<Categoria>($"./api/categoria/{id}");
        #endregion

        #region POST
        public async Task<HttpResponseMessage> Post(Categoria categoria) =>
            await _httpClient.PostAsJsonAsync("./api/categoria", categoria);
        #endregion

        #region PUT
        public async Task<HttpResponseMessage> Put(Categoria categoria, int id) =>
            await _httpClient.PutAsJsonAsync($"./api/categoria/{id}", categoria);
        #endregion
    }
}
