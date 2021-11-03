using FreeCourse.Services.Basket.Services;
using FreeCourse.Services.Basket.Settings;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

namespace FreeCourse.Services.Basket
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
            #region JWT

            // Burada di�er microservislerden farkl� olarak bir kullan�c�ya ihtiya� duyaca��m�zdan bu policy'i ekliyoruz. Daha sonra AddControllers'a options'� ekliyoruz.   
            var requireAuthorizePolicy =  new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

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
            services.AddHttpContextAccessor();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            services.AddScoped<IBasketService, BasketService>();
            services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));
            services.AddSingleton<RedisService>(sp =>
            {
                var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
                var redis = new RedisService(redisSettings.Host, redisSettings.Port);
                redis.Connect();
                return redis;
            });
            services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FreeCourse.Services.Basket", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FreeCourse.Services.Basket v1"));
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
