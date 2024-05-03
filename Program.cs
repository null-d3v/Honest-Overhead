using Serilog;
using WebMarkupMin.AspNetCore8;

#pragma warning disable CA1031

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

    webApplicationBuilder.Services
        .AddOptions();

    webApplicationBuilder.Services
        .AddResponseCompression();
    webApplicationBuilder.Services
        .AddWebOptimizer(
            assetPipeline =>
            {
                assetPipeline.MinifyCssFiles(
                    "css/**/*.css");
                assetPipeline.MinifyJsFiles(
                    "js/**/*.js");
                assetPipeline.AddCssBundle(
                    "/css/site.min.css",
                    "css/**/*.css");
                assetPipeline.AddJavaScriptBundle(
                    "/js/site.min.js",
                    "js/**/*.js");
            },
            options =>
            {
                options.EnableDiskCache = false;
            });
    webApplicationBuilder.Services
        .AddWebMarkupMin(
            options =>
            {
                options.AllowMinificationInDevelopmentEnvironment = true;
                options.AllowCompressionInDevelopmentEnvironment = true;
            })
        .AddHtmlMinification()
        .AddHttpCompression();

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

    await webApplication
        .RunAsync()
        .ConfigureAwait(false);
    return 0;
}
catch (Exception exception)
{
    Log.Fatal(exception, "Host terminated unexpectedly");
    return 1;
}
finally
{
    await Log
        .CloseAndFlushAsync()
        .ConfigureAwait(false);
}

#pragma warning restore CA1031