using FinanceWasm.Models.Account;
using System.Net.Http.Json;

namespace FinanceWasm.Services {
    public class AccountService {
        private readonly HttpClientService _httpClientService;

        public AccountService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        #region GET
        public async Task<Account> GetProfile() =>
            await _httpClientService.GetFromJsonAsync<Account>("./api/account/getuser");
        #endregion

        #region POST
        public async Task<HttpResponseMessage> Login(Login login) =>
            await _httpClientService.PostAsJsonAsync("./api/account/login", login);
        #endregion
    }
}
