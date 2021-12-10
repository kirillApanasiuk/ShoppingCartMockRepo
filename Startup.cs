using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingCart.ShoppingCart;
using ShoppingCart.ShoppingCart.interfaces;
using ShoppingCart.ShoppingCart.requestParsers.productService;
using ShoppingCart.ShoppingCart.services;
using System.Linq;
using Polly;
using System;

namespace HelloMicroservices
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Scan(selector => selector
                                     .FromAssemblyOf<Startup>()
                                     .AddClasses(c => c.Where(t => t != typeof(ProductCatalogClient) && t.GetMethods().All(m => m.Name != "<Clone>$")))
                                     .AsImplementedInterfaces());

            services.AddSingleton<ProductConverter>();
            services.AddHttpClient<IProductCatalogClient, ProductCatalogClient>()
              .AddTransientHttpErrorPolicy(p =>
                p.WaitAndRetryAsync(
                  3,
                  attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt))));

            services.AddControllers();
        }
       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(configure => configure.MapControllers());
        }
    }
}
