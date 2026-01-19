using Microsoft.EntityFrameworkCore;
using Notification.Application.Config;
using Notification.Infrastructure.Config;
using Notification.Infrastructure.Email;
using Notification.Infrastructure.Messaging.Configuration;
using Notification.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NotificationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddOptions<GmailOptions>().Bind(builder.Configuration.GetSection("Gmail"))
    .Validate(o => !string.IsNullOrWhiteSpace(o.Username), "Gmail:Username is required")
    .Validate(o => !string.IsNullOrWhiteSpace(o.AppPassword), "Gmail:AppPassword is required")
    .ValidateOnStart();
builder.Services.AddApplicationConfig();
builder.Services.AddInfrastructureConfig().AddMessagingConfig();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
