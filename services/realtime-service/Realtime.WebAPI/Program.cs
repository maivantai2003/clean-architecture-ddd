using Realtime.Application.Interfaces;
using Realtime.Infrastructure.Config;
using Realtime.Infrastructure.SignalR;
using User.Application.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddScoped<IRealtimePublisher, SignalRPublisher>();
builder.Services.AddInfrastructureConfig(builder.Configuration);
builder.Services.AddApplicationConfig();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(opt =>
//    {
//        opt.Events = new JwtBearerEvents
//        {
//            OnMessageReceived = ctx =>
//            {
//                var accessToken = ctx.Request.Query["access_token"];
//                var path = ctx.HttpContext.Request.Path;

//                if (!string.IsNullOrEmpty(accessToken)
//                    && path.StartsWithSegments("/realtime"))
//                {
//                    ctx.Token = accessToken;
//                }

//                return Task.CompletedTask;
//            }
//        };
//    });

//builder.Services.AddAuthorization();
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
app.MapHub<Realtime.WebAPI.Hubs.RealtimeHub>("/realtime");
app.MapControllers();

app.Run();
