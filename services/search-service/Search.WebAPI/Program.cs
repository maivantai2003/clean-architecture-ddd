using Microsoft.EntityFrameworkCore;
using Nest;
using Search.Application.Config;
using Search.Infrastructure.Config;
using Search.Infrastructure.Elasticsearch;
using Search.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SearchDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddInfrastructureConfig(builder.Configuration);
builder.Services.AddApplicationConfig();   
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var client = scope.ServiceProvider.GetRequiredService<IElasticClient>();
    //var options = scope.ServiceProvider.GetRequiredService<IOptions<ElasticOptions>>().Value;

    //await ElasticExtensions.CreateUserIndex(client, options.UserIndex);
    var initializer = scope.ServiceProvider.GetRequiredService<IElasticInitializer>();
        await initializer.InitializeAsync();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
