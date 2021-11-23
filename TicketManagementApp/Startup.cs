using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagementApp.Models;
using TicketManagementApp.Repositories.Abstract;
using TicketManagementApp.Repositories.Concrete;
using TicketManagementApp.Services.Abstract;
using TicketManagementApp.Services.Concrete;

namespace TicketManagementApp
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
            services.AddRazorPages();
            services.AddDbContext<TicketManagementDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });
            //Servisler tanýmlandý.
            services.AddScoped<ITicketDetailService, TicketDetailManager>();
            services.AddSingleton<IEmailSender, NetSmptMailManager>();
            services.AddScoped<ITicketService, TicketManager>();
            services.AddScoped<IEmployeeService, EmployeeManager>();
            services.AddScoped<ICustomerService, CustomerManager>();
            //Repositoryler tanýmlandý.
            services.AddScoped<ICustomerRepository, EFCustomerRepository>();
            services.AddScoped<IEmployeeRepository, EFEmployeeRepository>();
            services.AddScoped<ITicketDetailRepository, EFTicketDetailRepository>();
            services.AddScoped<ITicketRepository, EFTicketRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
