using Serilog;
using Serilog.Formatting.Elasticsearch;

namespace Sparrow.API;

public class Program
{
    public async static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>()
                .ConfigureLogging((_, logging) => { logging.ClearProviders(); })
                        .UseSerilog((ctx, cfg) =>
                        {
                            cfg.ReadFrom.Configuration(ctx.Configuration)
                                .Enrich.FromLogContext()
                                .Enrich.WithProperty("service_name", nameof(API));

                            if (ctx.HostingEnvironment.IsDevelopment())
                            {
                                cfg.WriteTo.Async(sinkCfg => sinkCfg.Console());
                            }
                            else
                            {
                                cfg.WriteTo.Async(sinkCfg => sinkCfg.Console(new ElasticsearchJsonFormatter()));
                            }
                        });
            }
                );
}