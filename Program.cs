using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;

try
{
    Log.Logger = new LoggerConfiguration()
        .CreateBootstrapLogger();

    var webApplicationBuilder = WebApplication.CreateBuilder(args);
    webApplicationBuilder.Configuration
        .AddJsonFile("appsettings.json", false, false);
    webApplicationBuilder.Configuration
        .AddEnvironmentVariables();

    webApplicationBuilder.Host.UseSerilog(
        (context, loggerConfiguration) =>
            loggerConfiguration
                .ReadFrom.Configuration(
                    context.Configuration));

    webApplicationBuilder.Services
        .AddSingleton(webApplicationBuilder.Configuration);

    webApplicationBuilder.Services.AddOptions();

    webApplicationBuilder.Services.AddResponseCompression();

    webApplicationBuilder.Services.AddRouting(
        routeOptions => routeOptions.LowercaseUrls = true);

    var mvcBuilder = webApplicationBuilder.Services
        .AddControllersWithViews();
    if (webApplicationBuilder.Environment.IsDevelopment())
    {
        mvcBuilder.AddRazorRuntimeCompilation();
    }

    using var webApplication = webApplicationBuilder.Build();

    webApplication.UseSerilogRequestLogging();

    if (webApplicationBuilder.Environment.IsDevelopment())
    {
        webApplication.UseDeveloperExceptionPage();
    }
    else
    {
        webApplication
            .UseExceptionHandler("/error/500");
        webApplication
            .UseStatusCodePagesWithReExecute("/error/{0}");
    }

    webApplication.UseResponseCompression();
    webApplication.UseStaticFiles();
    webApplication.UseRouting();

    webApplication.MapControllers();

    webApplication.Run();
    return 0;
}
catch (Exception exception)
{
    Log.Fatal(exception, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}