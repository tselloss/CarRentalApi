using CarRentalManagment.PostgresContext;
using Cars.Info.Interface;
using Cars.Info.Repository;
using HealthCheck;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Recomentation.Info.Interface;
using Recomentation.Info.Repository;
using RentInfo.Interface;
using RentInfo.Repository;
using Statistics.Info.Interface;
using Statistics.Info.Repository;
using System.Text;
using User.Info.Interface;
using User.Info.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHealthChecks().AddCheck<HealthChecks>("CustomCheck"); 
// Security Config
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]))
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Register DatabaseContextContrext
builder.Services.AddDbContext<PostgresDbContext>(options =>
options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration["ConnectionStrings:PostgreSQL"], b => b.MigrationsAssembly("CarRental")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUserInfo, UserInfoService>();
builder.Services.AddScoped<UserInfoService>();
builder.Services.AddScoped<ICars, CarsService>();
builder.Services.AddScoped<CarsService>();
builder.Services.AddScoped<IRental, RentalService>();
builder.Services.AddScoped<RentalService>();
builder.Services.AddScoped<IStatistics, StatisticsService>();
builder.Services.AddScoped<StatisticsService>();
builder.Services.AddScoped<IRecomentation, RecomentationService>();
builder.Services.AddScoped<RecomentationService>();

// Enable CORS to allow all origins, headers, and methods
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
    },
    ResponseWriter = async (context, report) =>
    {
        var result = new
        {
            Status = report.Status.ToString(),
            Checks = report.Entries.Select(entry => new
            {
                Component = entry.Key,
                Description = entry.Value.Description,
                Duration = entry.Value.Duration.TotalMilliseconds
            }),
        };

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
    }

});

app.MapControllers();

app.Run();
