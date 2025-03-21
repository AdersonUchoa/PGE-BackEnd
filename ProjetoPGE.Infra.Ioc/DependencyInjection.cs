using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProjetoPGE.Application.InterfaceServices;
using ProjetoPGE.Application.Mappings;
using ProjetoPGE.Application.Services;
using ProjetoPGE.Domain.Account;
using ProjetoPGE.Domain.InterfaceRepositories;
using ProjetoPGE.Domain.Models;
using ProjetoPGE.Infra.Data.Identity;
using ProjetoPGE.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPGE.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureSwagger();

            services.AddDbContext<PgedbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<ProcessoInterfaceRepository, ProcessoRepository>();
            services.AddScoped<DocumentoInterfaceRepository, DocumentoRepository>();
            services.AddScoped<PessoaInterfaceRepository, PessoaRepository>();
            services.AddScoped<PrazoInterfaceRepository, PrazoRepository>();

            services.AddScoped<ProcessoInterfaceService, ProcessoService>();
            services.AddScoped<DocumentoInterfaceService, DocumentoService>();
            services.AddScoped<PessoaInterfaceService, PessoaService>();
            services.AddScoped<PrazoInterfaceService, PrazoService>();
            services.AddScoped<IAuthenticate, AuthenticateService>();

            services.AddAutoMapper(typeof(ModelsToDTO));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["jwt:issuer"],
                    ValidAudience = configuration["jwt:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:secretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ProcuradorOuAdmin", policy =>
                    policy.RequireClaim("tipoPessoa", "Procurador", "Admin"));
                options.AddPolicy("ClienteOuAdmin", policy =>
                    policy.RequireClaim("tipoPessoa", "Cliente", "Admin"));
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireClaim("tipoPessoa", "Admin"));
                options.AddPolicy("ProcuradorOnly", policy =>
                    policy.RequireClaim("tipoPessoa", "Procurador"));
                options.AddPolicy("ClienteOnly", policy =>
                    policy.RequireClaim("tipoPessoa", "Cliente"));
            });

            return services;
        }
    }
}
