using System.Text.Json.Serialization;
using CadFuncionario.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CadFuncionario.Api.Configurations
{
    public static class ServicesConfiguration
    {
        public static void RegisterServicesApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = new HeaderApiVersionReader("version");
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("policyCors", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            var connectionString = configuration.GetSection("ConnectionsStrings").GetSection("DbContext").Value;

            services.AddDbContext<Context>(opt =>
                 opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                 b => b.MigrationsAssembly("CadFuncionario.Api")));

            services.AddControllers()
                .AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null)
                .AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CadFuncionario.Api", Version = "v1" });
            });
        }
    }
}