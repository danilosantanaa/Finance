using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FinanceWasm;
using FinanceWasm.Services;
using FinanceWasm.Helpers;
using FinanceWasm.Services.AuthProvider;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using FinanceWasm.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

#region HELPERS
builder.Services.AddScoped<SweetAlert>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
#endregion

#region AUTH PROVIDER
builder.Services.AddScoped<AuthProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<AuthProvider>()
);
#endregion

#region SERVICES
builder.Services.AddScoped<HttpClientService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<BancoService>();
builder.Services.AddScoped<TransacionadorService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<CobrancaService>();
builder.Services.AddScoped<ReciboService>();
#endregion

var app = builder.Build();

await app.RunAsync();
