using Finance.Domain;
using Finance.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Finance.Persistence.Contextos
{
    public class FinanceContextDatabase : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cobranca> Cobrancas { get; set; }
        public DbSet<Transacionador> Transacionadores { get; set; }
        public DbSet<Recibo> Recibos { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        public FinanceContextDatabase(DbContextOptions<FinanceContextDatabase> context) : base(context)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.RoleId, ur.UserId });

                userRole
                    .HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole
                    .HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            builder.Entity<Cobranca>()
                .HasOne(t => t.Transacionador)
                .WithMany(c => c.Cobrancas)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Recibo>()
                .HasOne(b => b.Banco)
                .WithMany(r => r.Recibos);
        }
    }
}