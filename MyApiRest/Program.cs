using MyApiRest.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using MyApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Registrar el servicio como singleton
builder.Services.AddSingleton<ProductService>();
// Agregar servicios para Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configurar EF Core con SQLite
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// Agregar controladores
builder.Services.AddControllers();
// validación para los tokens JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,                   // Verifica el emisor del token
            ValidateAudience = true,                 // Verifica la audiencia del token
            ValidateLifetime = true,                 // Verifica que el token no haya expirado
            ValidateIssuerSigningKey = true,         // Verifica la clave usada para firmar el token
            ValidIssuer = builder.Configuration["Jwt:Issuer"],        // Emisor válido configurado
            ValidAudience = builder.Configuration["Jwt:Audience"],    // Audiencia válida configurada
            IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)) // Clave de firma
        };

    });

builder.Services.AddAuthorization();
// metodo cors para que el navegador no bloquea dicha solicitud a menos que la API permita explícitamente el acceso desde ese origen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Especifica el origen permitido. cambiar puerto al mismo que la app frontend
               .AllowAnyHeader()// Permite cualquier encabezado HTTP (como Authorization, Content-Type, etc.)
               .AllowAnyMethod();// Permite cualquier método HTTP (GET, POST, PUT, DELETE, etc.)
    });
});

var app = builder.Build();

//app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    // Configurar Swagger solo en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Obtenemos el contecto de la base de datos
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    dbContext.Database.Migrate();
}


// Habilitar redirección HTTPS y controladores
app.UseHttpsRedirection();
app.UseCors("AllowAngularApp"); // Aplicar política CORS
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

