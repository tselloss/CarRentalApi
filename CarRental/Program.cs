using CarRentalManagment.PostgresContext;
using Cars.Info.Interface;
using Cars.Info.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RentInfo.Interface;
using RentInfo.Repository;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using User.Info.Interface;
using User.Info.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Security Config
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"{token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//Register DatabaseContextContrext
builder.Services.AddDbContext<PostgresDbContext>(options =>
options.UseNpgsql(builder.Configuration["ConnectionStrings:PostgreSQL"], b => b.MigrationsAssembly("CarRental")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUserInfo, UserInfoService>();
builder.Services.AddScoped<UserInfoService>();
builder.Services.AddScoped<ICars, CarsService>();
builder.Services.AddScoped<CarsService>();
builder.Services.AddScoped<IRental, RentalService>();
builder.Services.AddScoped<RentalService>();

var app = builder.Build();

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
