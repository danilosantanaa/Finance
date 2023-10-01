using Finance.API;
using Finance.Application;
using Finance.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddServices()
    .AddApplication(builder.Configuration)
    .AddPersistence(builder.Configuration);

var app = builder.Build();
await app
    .AddUses()
    .RunAsync();
