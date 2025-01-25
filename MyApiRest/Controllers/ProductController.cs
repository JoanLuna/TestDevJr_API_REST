using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 
using MyApi.Data;
using MyApiRest.Models;

namespace MyApiRest.Controllers; 

[ApiController] 
[Route("api/[controller]")] 
public class ProductController : ControllerBase 
{
    private readonly ProductDbContext _context; // Campo para interactuar con la base de datos a través del contexto.

    public ProductController(ProductDbContext context) // Constructor que recibe el contexto de base de datos como dependencia.
    {
        _context = context;
    }

    [HttpGet] // Indica que este método maneja solicitudes HTTP GET para obtener todos los productos.
    public async Task<IActionResult> GetAll()
    {
        var products = await _context.Products.ToListAsync(); // Obtiene todos los productos de la base de datos.
        return Ok(products); // Devuelve una respuesta HTTP 200 con los productos.
    }

    [HttpGet("{id:int}")] // Maneja solicitudes HTTP GET con un parámetro entero en la ruta (ID del producto).
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _context.Products.FindAsync(id); // Busca un producto por ID en la base de datos.
        if (product == null)
        {
            return NotFound(new { Message = "Producto no encontrado" }); // Devuelve una respuesta HTTP 404 con un mensaje.
        }
        return Ok(product); // Devuelve una respuesta HTTP 200 con el producto.
    }

    [HttpPost] // Maneja solicitudes HTTP POST para crear un nuevo producto.
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        if (!ModelState.IsValid) // Verifica si el modelo recibido es válido.
        {
            return BadRequest(ModelState); // Devuelve una respuesta HTTP 400 con los errores de validación.
        }

        _context.Products.Add(product); // Agrega el producto al contexto de base de datos.
        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos.

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product); // Devuelve una respuesta HTTP 201 con el producto creado.
    }

    [HttpPut("{id:int}")] // Maneja solicitudes HTTP PUT para actualizar un producto existente por ID.
    public async Task<IActionResult> Update(int id, [FromBody] Product product) // Recibe el ID y el producto actualizado.
    {
        if (!ModelState.IsValid) // Verifica si el modelo recibido es válido.
        {
            return BadRequest(ModelState); // Devuelve una respuesta HTTP 400 con los errores de validación.
        }

        var existingProduct = await _context.Products.FindAsync(id); // Busca el producto por ID en la base de datos.
        if (existingProduct == null) // Verifica si el producto no existe.
        {
            return NotFound(new { Message = "Producto no encontrado" }); // Devuelve una respuesta HTTP 404 con un mensaje.
        }

        // Actualiza las propiedades del producto existente.
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;

        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos.
        return NoContent(); // Devuelve una respuesta HTTP 204 (sin contenido).
    }

    [HttpDelete("{id:int}")] // Maneja solicitudes HTTP DELETE para eliminar un producto por ID.
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id); // Busca el producto por ID en la base de datos.
        if (product == null) // Verifica si el producto no existe.
        {
            return NotFound(new { Message = "Producto no encontrado" }); // Devuelve una respuesta HTTP 404 con un mensaje.
        }

        _context.Products.Remove(product); // Elimina el producto del contexto de base de datos.
        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos.
        return NoContent(); // Devuelve una respuesta HTTP 204 (sin contenido).
    }
}
