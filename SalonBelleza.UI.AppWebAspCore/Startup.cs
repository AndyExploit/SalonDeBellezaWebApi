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

//***********
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace SalonBelleza.UI.AppWebAspCore
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
            services.AddControllersWithViews();
            //Con este servicio estamos utilizando la vaiidacion por Login.
            // Configurar la autentificacion 
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie((o) =>
            {
                // Aca se redireccionara en caso que no posea permiso. Ira a la vista de Login.
                o.LoginPath = new PathString("/Usuario/login");
                                    //tiempo de duracion de las cookies, en este caso se han puesto 8 horas.
                o.ExpireTimeSpan = TimeSpan.FromHours(10);
                o.AccessDeniedPath= new PathString("/Usuario/AccessDenied");
                o.SlidingExpiration = true;
            });

            // Agregar el cliente HttpClient para consumir la Web API
            services.AddTransient(sp => new HttpClient
            {
                BaseAddress = new Uri("https://salonbellezawebapi.azurewebsites.net/api/")
            });
            //**

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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            //********************************
            // Permitir la autentificacion en la aplicacion Web
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
