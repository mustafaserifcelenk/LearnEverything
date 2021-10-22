using FreeCourse.Services.Catalog.Services;
using FreeCourse.Services.Catalog.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog
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
            #region OptionsPattern
            //AppSettings.json'� okuyup class'a aktarma: Options Pattern.
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));
            services.AddSingleton<IDatabaseSettings>(sp =>
            {
                // Service Provider(sp) veriyor, bu fonksiyon belirtti�imiz interface'i implement eden class d�n�yor
                // GetRequired Service'in Get Service'ten fark� d�n�� olmazsa hata f�rlat�r
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });

            // B�ylece IDatabaseSettings ge�ti�imizde GetSection ile okunan de�erler s�n�f olarak elimize ge�ecek
            #endregion
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter()); 
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FreeCourse.Services.Catalog", Version = "v1" });
            });

            #region JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                //token da��tmakla g�revli yap�
                // Bu uygulamaya private key ile imzalanan bir token geldi�inde uygulama IdentityServerUrl'i kullanarak endpointten public key alcak ve kar��la�t�rca
                options.Authority = Configuration["IdentityServerUrl"];
                // Bu service'e eri�ebilmek i�in hangi aud'e ihtiya� var, e�er kimlik bazl� kontrolde olsayd� onu da scope ile yapacakt�k
                options.Audience = "resource_catalog";
                // Https kontrol� devre d��� b�rakma
                options.RequireHttpsMetadata = false;
            });
                #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FreeCourse.Services.Catalog v1"));
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
