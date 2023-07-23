using Microsoft.EntityFrameworkCore;
using PostgresData;

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

//Register UserInfoContrext
builder.Services.AddDbContext<UserInfoContrext>(options =>
options.UseNpgsql(builder.Configuration["ConnectionStrings:PostgreSQL"], b => b.MigrationsAssembly("CarRentalManagment")));

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
