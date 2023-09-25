using System.Text;
using System.Text.Json.Serialization;
using Finance.Application;
using Finance.Application.Contratos;
using Finance.Domain.Identity;
using Finance.Persistence;
using Finance.Persistence.Contextos;
using Finance.Persistence.Contratos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FinanceContextDatabase>(context =>
    context.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnect"))
);

builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
})
.AddRoles<Role>()
.AddRoleManager<RoleManager<Role>>()
.AddSignInManager<SignInManager<User>>()
.AddRoleValidator<RoleValidator<Role>>()
.AddEntityFrameworkStores<FinanceContextDatabase>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

// Configurando o AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    )
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Finance.API",
        Version = "v1"
    });

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = @$"JWT Authorization usando o {JwtBearerDefaults.AuthenticationScheme}.
        Entre com '{JwtBearerDefaults.AuthenticationScheme} [espaço] então coloque seu token.
        Exemplo: '{JwtBearerDefaults.AuthenticationScheme} xxxx.xxxx.xxxx'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

/* Layers Depends Inject*/
#region PERSISTENCE LAYER
builder.Services.AddScoped<IGeneralPersistence, GeneralPersistence>();
builder.Services.AddScoped<IBancoPersistence, BancoPersistence>();
builder.Services.AddScoped<ICobrancaPersistence, CobrancaPersistence>();
builder.Services.AddScoped<IReciboPersistence, ReciboPersistence>();

builder.Services.AddScoped<ICategoriaPersistence, CategoriaPersistence>();

builder.Services.AddScoped<ITransacionadorPersistence, TransacionadorPersistence>();

builder.Services.AddScoped<IUserPersistence, UserPersistence>();
#endregion

#region SERVICE LAYER
builder.Services.AddScoped<IBancoService, BancoService>();
builder.Services.AddScoped<ICobrancaService, CobrancaService>();
builder.Services.AddScoped<IReciboService, ReciboService>();

builder.Services.AddScoped<ICategoriaService, CategoriaService>();

builder.Services.AddScoped<ITransacionadorService, TransacionadorService>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITokenService, TokenService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseReDoc(options =>
    {
        options.DocumentTitle = "REDOC API Documentation";
        options.SpecUrl = "/swagger/v1/swagger.json"; // "/api-docs/index.html"
    });
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true));

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
    RequestPath = new PathString("/resources")
});

app.Run();
