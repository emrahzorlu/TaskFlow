using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=taskflow.db"));

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();