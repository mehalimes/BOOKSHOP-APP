using bookshop.webapi.Contexts;
using bookshop.webapi.Models;
using bookshop.webapi.Services.Google;
using bookshop.webapi.Services.HttpClientServiceFolder;
using bookshop.webapi.Services.Iyzico;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient<HttpClientService>();
builder.Services.AddScoped<GoogleApiService>();
builder.Services.AddScoped<IyzicoService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;

    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = null;
    options.SignIn.RequireConfirmedAccount = false;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.MapControllers();

app.UseCors("AllowAll");

app.Run();
