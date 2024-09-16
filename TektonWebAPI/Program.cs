var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Host.ConfigureLogs(builder.Configuration);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddCustomMappings();
builder.Services.AddValidationServices();
builder.Services.AddApiDocumentation();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.AddCustomServices();
builder.Services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly));
builder.Services.AddAuthorization();


// It could be "Day", "Month", etc.
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