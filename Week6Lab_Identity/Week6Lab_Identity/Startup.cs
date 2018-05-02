using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Week6Lab_Identity.Data;
using Week6Lab_Identity.Models;
using Week6Lab_Identity.Models.Policies;


namespace Week6Lab_Identity
{
    public class Startup
    {
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DbCtx>()
                .AddDefaultTokenProviders();

            services.AddDbContext<DbCtx>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultString"));
            });

            services.AddAuthorization(options => 
            {
                options.AddPolicy("Educational", policy => policy.Requirements.Add(
                    new EmailEndRequirement(@"^[a-zA-Z0-9\._]+@\w{1,5}\.edu$")));
                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
            app.UseAuthentication();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
