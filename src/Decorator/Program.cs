using Decorator;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostBuilderContext, logger) => logger
    .ReadFrom.Configuration(hostBuilderContext.Configuration)
    .WriteTo.Async(config => config.Console())
);

builder.Services.AddLogging();
builder.Services.AddMemoryCache();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddSingleton<IProductDbContext, ProductDbContext>()

    .AddTransient<IProductRepository, ProductRepository>()
    .Decorate<IProductRepository, CachedProductRepository>();    

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.MapControllers();

app.MapGet("/api/products", (IProductRepository repository) => Results.Ok(repository.GetAll()));

app.MapGet("/api/products/{productId}", ([FromRoute] int productId, IProductRepository repository) => Results.Ok(repository.GetById(productId)));

app.Run();