using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Prometheus;
using Serilog;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
//Log.Logger = new LoggerConfiguration()
//    .Enrich.FromLogContext()
//    .Enrich.WithProperty("Service", Environment.GetEnvironmentVariable("SERVICE_NAME"))
//    .Enrich.WithActivityId()
//    .WriteTo.Console(outputTemplate:
//        "[{Timestamp:HH:mm:ss} {Level:u3}] [{Service}] [{TraceId}] {Message:lj}{NewLine}{Exception}")
//    .CreateLogger();
//builder.Services.AddOpenTelemetry()
//    .WithTracing(tracing =>
//    {
//        tracing
//            .AddAspNetCoreInstrumentation()
//            .AddHttpClientInstrumentation()
//            .AddOtlpExporter(opt =>
//            {
//                opt.Endpoint = new Uri(builder.Configuration["Jaeger:Uri"]);
//            });
//    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRCors", policy =>
    {
        policy
            .WithOrigins("http://127.0.0.1:5500")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;

                if (!string.IsNullOrEmpty(accessToken)
                    && path.StartsWithSegments("/realtime"))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)
            ),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();
//builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(builder.Configuration["Redis:ConnectionString"]));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration =
    builder.Configuration["Redis:ConnectionString"];
    options.InstanceName = "gateway:";
});
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
var app = builder.Build();

//builder.Host.UseSerilog();
app.UseWebSockets();
app.UseRouting();
app.UseCors("SignalRCors");
app.UseHttpMetrics();
//app.MapMetrics("/metrics");
//app.UseMiddleware<CorrelationIdMiddleware>();
//app.UseMiddleware<GatewayMetricsMiddleware>();
//app.UseMiddleware<AccessLogMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();
app.Run();
