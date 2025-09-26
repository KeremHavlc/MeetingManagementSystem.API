using MeetingManagementSystem.Application.Abstractions;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Infrastructure.Authentication;
using MeetingManagementSystem.Persistence;
using MeetingManagementSystem.Persistence.Context;
using MeetingManagementSystem.WebAPI.OptionsSetup;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Db baðlantýsý
var connectionString = builder.Configuration.GetConnectionString("sqlServer");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

//Persistence Tier Service Registration
builder.Services.AddPersistenceService();

//Controller baþka katmana eklendi!
builder.Services.AddControllers()
    .AddApplicationPart(typeof(MeetingManagementSystem.Presentation.AssemblyReference).Assembly);

//Identity ekle
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.SignIn.RequireConfirmedEmail = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
})
.AddSignInManager<SignInManager<AppUser>>() //SignInManager için
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddDataProtection();

//MediatR Service Registration
builder.Services.AddMediatR(cfr =>
{
    cfr.RegisterServicesFromAssembly(typeof(MeetingManagementSystem.Application.AssemblyReference).Assembly);
});

//Jwt Configurations Service Registration
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
//Jwt Ayarlarý
builder.Services.AddAuthentication().AddJwtBearer();
//JwtProvider Service Registration
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
