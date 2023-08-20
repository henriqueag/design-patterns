using DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostBuilderContext, logger) => logger
    .ReadFrom.Configuration(hostBuilderContext.Configuration)
    .WriteTo.Async(config => config.Console())
);

builder.Services.AddLogging();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<ScopedService>()
    .AddSingleton<SingletonService>();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.MapControllers();

app.MapGet("api/scoped-service", (ScopedService service, Serilog.ILogger logger) =>
{
    new ClientService1(service, logger).Execute();
    new ClientService2(service, logger).Execute();
    new ClientService3(service, logger).Execute();
    return Results.Ok();
});

app.MapGet("api/singleton-service", (SingletonService service, Serilog.ILogger logger) =>
{
    new ClientSingletonService1(service, logger).Execute();
    return Results.Ok();
});

app.Run();