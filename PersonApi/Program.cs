using Microsoft.EntityFrameworkCore;
using PersonApi.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // krävs för Swagger
builder.Services.AddSwaggerGen();           // generera OpenAPI

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));


var app = builder.Build();

app.UseSwagger();                           // /swagger/v1/swagger.json
app.UseSwaggerUI();                         // /swagger

app.MapControllers();

app.Run();
