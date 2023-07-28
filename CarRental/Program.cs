using CarRentalManagment.PostgresContext;
using Cars.Info.Interface;
using Cars.Info.Repository;
using Microsoft.EntityFrameworkCore;
using RentInfo.Interface;
using RentInfo.Repository;
using User.Info.Interface;
using User.Info.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
