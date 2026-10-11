using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Observability;
using Serilog;
using Shared.Contracts.Grpcs;
using StackExchange.Redis;
using User.Application.Config;
using User.Infrastructure.Caching;
using User.Infrastructure.Config;
using User.Infrastructure.Persistence;
using User.Infrastructure.Security;
using User.WebAPI.Middlewares;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddObservability();
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<RedisOptions>(builder.Configuration.GetSection(RedisOptions.SectionName));
//builder.Services.Configure<RedisOptions>(builder.Configuration.GetSection(RedisOptions);
builder.Services.AddOptions<JwtOptions>()
    .Bind(builder.Configuration.GetSection("Jwt"))
    .Validate(o => !string.IsNullOrWhiteSpace(o.SecretKey), "Jwt:SecretKey is required")
    .Validate(o => !string.IsNullOrWhiteSpace(o.Issuer), "Jwt:Issuer is required")
    .Validate(o => !string.IsNullOrWhiteSpace(o.Audience), "Jwt:Audience is required")
    .ValidateOnStart();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var options = sp.GetRequiredService<IOptions<RedisOptions>>().Value;
    return ConnectionMultiplexer.Connect(options.ConnectionString);
});
builder.Services.AddInfrastructureConfig(builder.Configuration).AddAuthenticationConfig();
builder.Services.AddApplicationConfig();
builder.Services.AddGrpcClient<AuthGrpc.AuthGrpcClient>(o =>
{
    o.Address = new Uri(builder.Configuration["GrpcServices:AuthService:Address"]);
});
var authBaseUrl = builder.Configuration["AuthService:BaseUrl"];
if (string.IsNullOrWhiteSpace(authBaseUrl))
{
    throw new InvalidOperationException(
        "AuthService:BaseUrl is not configured");
}
builder.Services.AddHttpClient("AuthClient", c =>
{
    c.BaseAddress = new Uri(authBaseUrl);
});
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter token"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
