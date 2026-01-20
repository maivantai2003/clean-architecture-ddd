using APIGateway.Middlewares;
using OpenTelemetry.Trace;
using Prometheus;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Service", Environment.GetEnvironmentVariable("SERVICE_NAME"))
    .Enrich.WithActivityId()
    .WriteTo.Console(outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] [{Service}] [{TraceId}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddOtlpExporter(opt =>
            {
                opt.Endpoint = new Uri(builder.Configuration["Jaeger:Uri"]);
            });
    });

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>{
        options.Authority = builder.Configuration["Jwt:Authority"];
        options.Audience = builder.Configuration["Jwt:Audience"];
        options.RequireHttpsMetadata =bool.Parse(builder.Configuration["Jwt:RequireHttpsMetadata"]!);
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

builder.Host.UseSerilog();
app.UseRouting();
app.UseHttpMetrics();
app.MapMetrics("/metrics");
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<GatewayMetricsMiddleware>();
app.UseMiddleware<AccessLogMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
