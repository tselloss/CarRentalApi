using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PostgresData;
using Users.Interface;
using Users.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration setup
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configuration = builder.Configuration;

////JwtAuthorization
//builder.Services.AddAuthentication(_ =>
//{
//    _.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//    _.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    _.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(_ =>
//{
//    var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
//    _.SaveToken = true;
//    _.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = configuration["JWT:Issuer"],
//        ValidAudience = configuration["JWT:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Key)
//    };
//});
//builder.Services.AddAuthentication();

//Register DatabaseContextContrext
builder.Services.AddDbContext<PostgresContext>(options =>
options.UseNpgsql(builder.Configuration["ConnectionStrings:PostgreSQL"], b => b.MigrationsAssembly("CarRentalManagment")));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUserInfo, UserInfoService>();

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
