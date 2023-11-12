using FinanceWasm.Models.Account;

using Microsoft.AspNetCore.Components.Forms;

using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace FinanceWasm.Services
{
    public class AccountService
    {
        private readonly HttpClientService _httpClientService;

        public AccountService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        #region GET
        public async Task<Account> GetProfile() =>
            await _httpClientService.GetFromJsonAsync<Account>("./api/account/get-user");
        #endregion

        #region POST
        public async Task<HttpResponseMessage> Login(Login login) =>
            await _httpClientService.PostAsJsonAsync("./api/account/login", login);

        public async Task<HttpResponseMessage> Register(Register register) =>
            await _httpClientService.PostAsJsonAsync("./api/account/register", register);
        #endregion

        #region PUT
        public async Task<HttpResponseMessage> UpdateUser(Account account) =>
            await _httpClientService.PutAsJsonAsync("./api/account/update-user", account);

        public async Task<HttpResponseMessage> EnviarFotoPerfil(IBrowserFile file)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream(file.Size));
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "perfil",
                FileName = file.Name
            };

            content.Add(fileContent, "file", file.Name);

            return await _httpClientService.PutAsync("./api/account/foto-perfil", content);
        }
        #endregion
    }
}
