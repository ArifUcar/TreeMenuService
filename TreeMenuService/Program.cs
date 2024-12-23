using DataAccesLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using TreeMenuService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer("server=DESKTOP-I79O76V;database=Category;integrated security=true;TrustServerCertificate=True"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:4200")  // Yaln�zca bu kayna�a izin ver
            .AllowAnyMethod()
            .AllowAnyHeader());
});
// CORS konfig�rasyonu
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()    // Herhangi bir kaynaktan gelen iste�e izin verir
            .AllowAnyMethod()    // GET, POST, PUT, DELETE vb. t�m HTTP metodlar�na izin verir
            .AllowAnyHeader());  // Herhangi bir HTTP ba�l���na izin verir
});

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll"); 
app.MapControllers();

app.Run();
