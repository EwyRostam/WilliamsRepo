using System.Reflection;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var config = new ConfigurationBuilder()
.AddJsonFile("appsettings.json", false, true)
.AddUserSecrets(Assembly.GetExecutingAssembly(), true)
.Build();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CanineContext>(options =>
    options.UseSqlServer(config.GetConnectionString("CanineContext") ?? throw new InvalidOperationException("Connection string 'CanineContext' not found.")));


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
