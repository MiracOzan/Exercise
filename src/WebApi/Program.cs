using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration = null;

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(
    x => 
        x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(x => 
    x.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin().AllowCredentials());

app.MapControllers();

app.Run();