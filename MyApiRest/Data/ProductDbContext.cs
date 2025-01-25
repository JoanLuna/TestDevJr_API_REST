using Microsoft.EntityFrameworkCore;
using MyApiRest.Models;

namespace MyApi.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }// Contecto de la base de datos

    public DbSet<Product> Products { get; set; }// Tabla de productos
    public DbSet<User> Users { get; set; } // Tabla de usuarios
}
