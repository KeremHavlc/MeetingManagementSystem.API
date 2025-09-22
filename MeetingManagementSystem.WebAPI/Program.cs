using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Persistence;
using MeetingManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Db baðlantýsý
var connectionString = builder.Configuration.GetConnectionString("sqlServer");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

//Persistence Tier Service Registration
builder.Services.AddPersistenceService();

//Identity Service Registration
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
})
.AddEntityFrameworkStores<AppDbContext>();

//Uygulamaya baþka katmanda controllerlarýn devam edeceðini belirtilen kýsým
builder.Services.AddControllers()
    .AddApplicationPart(typeof(MeetingManagementSystem.Presentation.AssemblyReference).Assembly);

//MediatR Service Registration
builder.Services.AddMediatR(cfr => cfr.RegisterServicesFromAssembly(typeof(MeetingManagementSystem.Application.AssemblyReference).Assembly));


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
