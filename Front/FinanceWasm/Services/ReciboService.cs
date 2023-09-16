using FinanceWasm.Models;
using System.Net.Http.Json;

namespace FinanceWasm.Services {
    public class ReciboService {
        private readonly HttpClientService _httpClient;

        public ReciboService(HttpClientService httpClient)
        {
            _httpClient = httpClient;
        }

        #region GET
        public async Task<List<Recibo>> GetAll(int cobrancaId) =>
            await _httpClient.GetFromJsonAsync<List<Recibo>>($"./api/recibo/{cobrancaId}");

        public async Task<Recibo> GetById(int cobrancaId, int reciboId) =>
            await _httpClient.GetFromJsonAsync<Recibo>($"./api/recibo/{cobrancaId}/{reciboId}");

        public async Task<HttpResponseMessage> Cancelar(int cobrancaId, int reciboId) =>
            await _httpClient.GetAsync($"api/Recibo/{cobrancaId}/{reciboId}/cancelar");
        #endregion

        #region POST
        public async Task<HttpResponseMessage> EnviarRecibos(int cobrancaId, List<Recibo> recibos) =>
            await _httpClient.PutAsJsonAsync($"./api/recibo/{cobrancaId}", recibos);
        #endregion

        #region PUT

        #endregion
    }
}
