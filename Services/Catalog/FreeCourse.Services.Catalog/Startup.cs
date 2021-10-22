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
            //AppSettings.json'ý okuyup class'a aktarma: Options Pattern.
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));
            services.AddSingleton<IDatabaseSettings>(sp =>
            {
                // Service Provider(sp) veriyor, bu fonksiyon belirttiðimiz interface'i implement eden class dönüyor
                // GetRequired Service'in Get Service'ten farký dönüþ olmazsa hata fýrlatýr
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });

            // Böylece IDatabaseSettings geçtiðimizde GetSection ile okunan deðerler sýnýf olarak elimize geçecek
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
                //token daðýtmakla görevli yapý
                // Bu uygulamaya private key ile imzalanan bir token geldiðinde uygulama IdentityServerUrl'i kullanarak endpointten public key alcak ve karþýlaþtýrca
                options.Authority = Configuration["IdentityServerUrl"];
                // Bu service'e eriþebilmek için hangi aud'e ihtiyaç var, eðer kimlik bazlý kontrolde olsaydý onu da scope ile yapacaktýk
                options.Audience = "resource_catalog";
                // Https kontrolü devre dýþý býrakma
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
