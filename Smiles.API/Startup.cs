using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Smiles.API.Helpers;
using Smiles.BL;
using Smiles.Core;
using Smiles.Core.Services;
using Smiles.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smiles.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISmilesService, SmilesService>();
            services.AddDbContext<SmilesDbContext>(options => 
                options.UseNpgsql(
                    Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("Smiles.DAL")
                )
            );
            services.AddApiVersioning(
                versionConfig =>
                {
                    versionConfig.DefaultApiVersion = new ApiVersion(1, 0);
                    versionConfig.AssumeDefaultVersionWhenUnspecified = true;
                    versionConfig.ReportApiVersions = true;
                }
            );
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<CustomOperationFilter>();
                c.ResolveConflictingActions(apiDescription => apiDescription.Last());
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SMILES API", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "SMILES API", Version = "v2" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SmilesDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SMILES API v1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "SMILES API v2");
                }
                );
            }

            db.Database.Migrate();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
