using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FinanceWasm.Models;

namespace FinanceWasm.Services
{
    public class BancoService
    {
        private readonly HttpClientService _httpClient;

        public BancoService(HttpClientService httpClient)
        {
            _httpClient = httpClient;
        }

        #region GET
        public async Task<List<Banco>> GetAll() =>
            await _httpClient.GetFromJsonAsync<List<Banco>>("./api/banco");

        public async Task<Banco> GetById(int id) =>
            await _httpClient.GetFromJsonAsync<Banco>($"./api/banco/{id}");
        #endregion

        #region POST
        public async Task<HttpResponseMessage> Post(Banco banco) =>
            await _httpClient.PostAsJsonAsync("./api/banco", banco);
        #endregion

        #region PUT
        public async Task<HttpResponseMessage> Put(Banco banco, int id) =>
            await _httpClient.PutAsJsonAsync($"api/banco/{id}", banco);
        #endregion
    }
}