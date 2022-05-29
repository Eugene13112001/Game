using Microsoft.EntityFrameworkCore;
using WebApplication34.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connection = "Server=WIN-FGJF0FKB7DT\\SQLEXPRESS;Database=Application;User Id = Eugene; Password = poltavka46z;Trusted_Connection=False;MultipleActiveResultSets=true;";
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseDeveloperExceptionPage();

app.UseAuthorization();

app.MapControllers();

app.Run();
