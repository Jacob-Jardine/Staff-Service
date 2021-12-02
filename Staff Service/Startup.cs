using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Staff_Service.Context;
using Staff_Service.Helpers;
using Staff_Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Service
{
    public class Startup
    {
        private IWebHostEnvironment _environment = null;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            if (_environment.IsDevelopment()) 
            {
                services.AddSingleton<IStaffRepository, FakeStaffRepository>();
            }
            else if (_environment.IsStaging() || _environment.IsProduction()) 
            {
                services.AddDbContext<StagingContext>(options =>
                {
                    var cs = Configuration.GetConnectionString("DbConnection");
                    options.UseSqlServer(cs);
                });
                services.AddDbContext<ProductionContext>(options =>
                {
                    var cs = Configuration.GetConnectionString("DbConnection");
                    options.UseSqlServer(cs);
                });
                services.AddScoped<IStaffRepository, SqlStaffRepository>();
            }
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                options.Audience = Configuration["Auth0:Audience"];
            });

            services.AddAuthorization(o =>
            {
                o.AddPolicy("ReadStaff", policy =>
                    policy.RequireClaim("permissions", "read:staff"));
                o.AddPolicy("ReadAllStaff", policy =>
                    policy.RequireClaim("permissions", "read:staffs"));
                o.AddPolicy("CreateStaff", policy =>
                    policy.RequireClaim("permissions", "add:staff"));
                o.AddPolicy("UpdateStaff", policy =>
                    policy.RequireClaim("permissions", "edit:staff"));
                o.AddPolicy("DeleteStaff", policy =>
                    policy.RequireClaim("permissions", "delete:staff"));
            });
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMemoryCache();
            services.AddSingleton<IStaffMemoryCache, StaffMemoryCache>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStaffMemoryCache memoryCache)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            memoryCache.AutomateCache();
        }
    }
}
