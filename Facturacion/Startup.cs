using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facturacion.Data;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Facturacion.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Facturacion.Data.Contracts;
using Facturacion.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Facturacion.Domain;
using Serilog;
namespace Facturacion
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddDbContext<EasyStcokDBContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("EasyStockDB")));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPosRepository, PosRepository>();
            services.AddScoped<IIdentityDocumentTypeRepository, IdentityDocumentTypeRepository>();
            services.AddScoped<IVatConditionRepository, VatConditionRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();
            services.AddSingleton<TokenService>();


            var JwtSettings = Configuration.GetSection("JwtSettings");

            services.AddCors(opt =>
            {
                //opt.AddPolicy("CorsPolicy", policy =>
                //{
                //    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(JwtSettings.GetValue<string>("Issuer")); // uso esta clave porque es el emisor/origen
                //});

                opt.AddPolicy(name: MyAllowSpecificOrigins, policy =>
                {
                    policy.WithOrigins(JwtSettings.GetValue<string>("Issuer"), "http://app.inventario-facil.com", "app.inventario-facil.com", "http://inventario-facil.com", "inventario-facil.com", "http://localhost:3000", "http://192.168.0.53:3000/")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod(); // uso esta clave porque es el emisor/origen
                });
            });


            //acceder a la confi de jwt
            string secretKey = JwtSettings.GetValue<string>("SecretKey");
            int minutes = JwtSettings.GetValue<int>("MinutesToExpiration");
            string issuer = JwtSettings.GetValue<string>("Issuer");
            string audience = JwtSettings.GetValue<string>("Audience");
            var key = Encoding.ASCII.GetBytes(secretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(minutes)
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var JwtSettings = Configuration.GetSection("JwtSettings");
            app.UseHttpsRedirection();

            loggerFactory.AddSerilog();
            app.UseRouting();
            app.UseCors(builder =>
            {
                builder.WithOrigins(JwtSettings.GetValue<string>("Issuer"), "http://app.inventario-facil.com", "app.inventario-facil.com", "http://inventario-facil.com", "inventario-facil.com", "http://localhost:3000", "http://192.168.0.53:3000/")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("Content-Disposition");

            });

            //app.UseAuthorization();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
