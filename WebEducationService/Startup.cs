using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebEducationService.Data;


namespace WebEducationService {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public readonly string DefaultCorsPolicy = "_defaultCorsPolicy";
        public string[] AllowOrigins = { "http://localhost:4200" };//Add another url here if you want to add it to the list
        public string[] AllowMethods = { "GET", "POST", "PUT", "DELETE" };

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
                services.AddControllers();
                    services.AddDbContext<EdDbContext>(options => {
                        options.UseLazyLoadingProxies();
                        options.UseSqlServer(Configuration.GetConnectionString("EdDbContext"));
                    });
            services.AddCors(option =>
                option.AddPolicy(DefaultCorsPolicy, x =>
                    x.WithOrigins(AllowOrigins).WithMethods(AllowMethods).AllowAnyMethod()
                )
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            
            app.UseCors(DefaultCorsPolicy);  //This is what allows the cors to occur

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            //Programatic way to get migrations to run, to external servers once you leave class environment
            using var scope = app.ApplicationServices
                        .GetRequiredService<IServiceScopeFactory>()
                            .CreateScope();
            scope.ServiceProvider.GetService<EdDbContext>()
                                .Database.Migrate();
        }
    }
}
