using System.Text;
using Finance.Application.Contratos;
using Finance.Application.Services;
using Finance.Domain.Identity;
using Finance.Persistence.Contextos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Finance.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        services.AddIdentityCore<User>(options =>
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

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"])),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true
                 };
             });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<IBancoService, BancoService>();
        services.AddScoped<ICobrancaService, CobrancaService>();
        services.AddScoped<IReciboService, ReciboService>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<ITransacionadorService, TransacionadorService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUploadService, UploadService>();

        return services;
    }
}
