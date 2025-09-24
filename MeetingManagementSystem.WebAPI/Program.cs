using MeetingManagementSystem.Persistence;
using MeetingManagementSystem.Persistence.Context;
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
