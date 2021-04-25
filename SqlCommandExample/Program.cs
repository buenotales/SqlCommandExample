using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlCommandExample.Repositories;
using System;

namespace SqlCommandExample
{
    class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            using (IServiceScope serviceScope = host.Services.CreateScope())
            {
                IServiceProvider provider = serviceScope.ServiceProvider;

                IApp app = provider.GetRequiredService<IApp>();
                app.Execute();
            }

            host.Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) => ConfigureServices(services: services));

        static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddTransient<IApp, App>()
                .AddTransient<IProductRespository, ProductRepository>();
        }
    }
}
