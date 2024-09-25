using System.Security.Cryptography.X509Certificates;
using TektonWebAPI.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ConfigureHttpsDefaults(httpsOptions =>
//    {
//        httpsOptions.ServerCertificate = new X509Certificate2(
//            Path.Combine("/https", "aspnetapp.pfx"),
//            "password");
//    });
//});


builder.Host.ConfigureLogs(builder.Configuration);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(); // Some services depends on DI coming from infrastructure, so this layer must be added after infra.


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseRequestContextLogging();

app.UseRequestExceptionHandling();

app.UseRequestTiming();

app.MapProductEndpoints();

app.MapAuthEndpoints();

app.Run();