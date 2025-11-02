using FluentValidation;
using MediatR;
using MeetingManagementSystem.Application.Abstractions;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Infrastructure;
using MeetingManagementSystem.Infrastructure.Authentication;
using MeetingManagementSystem.Persistence;
using MeetingManagementSystem.Persistence.Context;
using MeetingManagementSystem.WebAPI.Hubs;
using MeetingManagementSystem.WebAPI.Middleware;
using MeetingManagementSystem.WebAPI.OptionsSetup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//Db baðlantýsý
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure())
);
//Persistence Tier Service Registration
builder.Services.AddPersistenceService();
//Infrastructure Tier Service Registration
builder.Services.AddInfrastructureService(builder.Configuration);

//Controller baþka katmana eklendi!
builder.Services.AddControllers()
    .AddApplicationPart(typeof(MeetingManagementSystem.Presentation.AssemblyReference).Assembly);
// HttpContextAccessor Service Registration
builder.Services.AddHttpContextAccessor();
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

//FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(MeetingManagementSystem.Application.AssemblyReference).Assembly);
// Validation pipeline (MediatR pipeline'ýna ekle)
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MeetingManagementSystem.Application.Behaviors.ValidationBehavior<,>));


builder.Services.AddDataProtection();

//MediatR Service Registration
builder.Services.AddMediatR(cfr =>
{
    cfr.RegisterServicesFromAssembly(typeof(MeetingManagementSystem.Application.AssemblyReference).Assembly);
});
//Authentication Service

//Jwt Configurations Service Registration
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
//Jwt Ayarlarý
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer();
builder.Services.AddAuthorization();
//JwtProvider Service Registration
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});
//SignalR Service Registration
builder.Services.AddSignalR();
// CORS ayarlarý
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenCorsPolicy", policy =>
    {
        policy.WithOrigins(
            "https://meetingmanagementsystemclient.azurewebsites.net",
            "https://meeting-management-system-client.vercel.app",
            "http://localhost:5173"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});
//Error Middleware Service Registration
builder.Services.AddTransient<ExceptionMiddleware>();
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
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseCors("OpenCorsPolicy"); 

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");
app.MapControllers();

app.MapControllers();

app.Run();
