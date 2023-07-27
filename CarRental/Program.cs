using CarRentalManagment.PostgresContext;
using Microsoft.EntityFrameworkCore;
using Users.Interface;
using Users.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Register DatabaseContextContrext
builder.Services.AddDbContext<PostgresDbContext>(options =>
options.UseNpgsql(builder.Configuration["ConnectionStrings:PostgreSQL"]));


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

app.UseAuthorization();

app.MapControllers();

app.Run();
