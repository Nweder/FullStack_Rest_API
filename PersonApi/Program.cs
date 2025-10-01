using Microsoft.EntityFrameworkCore;
using PersonApi.Data;

var builder = WebApplication.CreateBuilder(args);

// ---- Services ----
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddCors(p => p.AddDefaultPolicy(b =>
    b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// ---- Build ----
var app = builder.Build();

// ---- Middleware ----
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();            // CORS före MapControllers

app.UseDefaultFiles();    // servera wwwroot/index.html
app.UseStaticFiles();

app.MapControllers();

// ---- Run ----
app.Run();
