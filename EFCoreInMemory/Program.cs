using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace EFCoreInMemory
{
    internal class Program
    {
        static void Main( string[] args )
        {
            CreateHostBuilder().Build().Run();
        }

        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices( ( context, services ) =>
                {
                    services.AddDbContextFactory<TestContext>( lifetime: ServiceLifetime.Transient );
                    services.AddTransient<TestController>();
                    services.AddHostedService<TestService>();
                } );
        }
    }
}