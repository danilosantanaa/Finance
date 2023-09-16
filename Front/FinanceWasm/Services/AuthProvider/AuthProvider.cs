using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SistemaFinanceiro.Helpers;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace FinanceWasm.Services.AuthProvider {
    public class AuthProvider : AuthenticationStateProvider {
        private readonly HttpClient _httpClientService;
        private readonly ILocalStorageService _localStorage;
        public readonly string JWT_TOKEN_KEY = "jwt_token_key";
        public readonly string REMEMBER_LOGIN_KEY = "remember_login_key";

        public AuthProvider(HttpClientService httpClientService, ILocalStorageService localStorage)
        {
            _httpClientService = httpClientService;
            _localStorage = localStorage;
        }

        private AuthenticationState notAuthentication =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsStringAsync(JWT_TOKEN_KEY);

            if (string.IsNullOrEmpty(token)) {
                return notAuthentication;
            }

            return await CreateAuthenticationState(token);
        }

        public async Task<AuthenticationState> CreateAuthenticationState(string token)
        {
            _httpClientService.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var claims = JwtParser.ParseClaimsFromJwt(token);

            var expiry = claims.Where(claim => claim.Type.Equals("exp")).FirstOrDefault();
            if (expiry == null)
                return notAuthentication;

            var datetime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiry.Value));
            if(datetime.UtcDateTime <= DateTime.UtcNow) {
                await RemoveTokenJwtStorager();
                return notAuthentication;
            }

            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        claims,
                        "jwtAuthType"
                    )
                )
            );
        }

        public async Task Login(string token)
        {
            await _localStorage.SetItemAsStringAsync(JWT_TOKEN_KEY, token);

            var authState = await CreateAuthenticationState(token);

            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await RemoveTokenJwtStorager();

            NotifyAuthenticationStateChanged(Task.FromResult(notAuthentication));
        }

        async Task RemoveTokenJwtStorager()
        {
            await _localStorage.RemoveItemAsync(JWT_TOKEN_KEY);
            _httpClientService.DefaultRequestHeaders.Authorization = null;
        }
    }
}