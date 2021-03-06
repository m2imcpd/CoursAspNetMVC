﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession();
            services.AddDbContext<DataDbContext>(ServiceLifetime.Singleton, ServiceLifetime.Singleton);
            services.AddTransient<IResizeImageService, ResizeImageService>();
            services.AddTransient<IServicePanier, ServicePanier>();
            services.AddSingleton<ILoginService, LoginService>();
            services.AddHttpContextAccessor();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("client", pol=> {
                    pol.Requirements.Add(new AccessRequirement(2));
                });
                //Ajouter une police admin pour sécuriser les pages ajouter catégorie et produit
                options.AddPolicy("admin", pol => {
                    pol.Requirements.Add(new AccessRequirement(3));
                });
            });
            services.AddSingleton<IAuthorizationHandler, AccessHandler>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "updateQtyProduit",
                    template: "UpdateQty/{id}/{qty}",new { controller="Panier", action="UpdateQty"});
            });
        }
    }
}
