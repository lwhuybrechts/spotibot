using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SpotiBot.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        FunctionsApplication.CreateBuilder(args)
            .ConfigureFunctionsWebApplication()
            .AddServices()
            .Build()
            .Run();
    }

    private static FunctionsApplicationBuilder AddServices(this FunctionsApplicationBuilder builder)
    {
        builder
            .Services
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights()
            .AddOptions()
            .AddMapper()
            .AddStorage()
            .AddServices();

        return builder;
    }
}