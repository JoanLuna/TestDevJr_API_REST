namespace MyApiRest.Models; // Define el espacio de nombres para agrupar las clases relacionadas con los modelos de datos.

using System.ComponentModel.DataAnnotations; // Proporciona atributos para la validación de modelos.

public class Product // Clase que representa el modelo de producto.
{
    public int Id { get; set; } // Propiedad para el identificador único del producto.

    [Required(ErrorMessage = "El nombre es obligatorio")] // Valida que el campo Name no sea nulo o vacío.
    [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")] // Restringe la longitud máxima del nombre.
    public string Name { get; set; } = string.Empty; // Propiedad para el nombre del producto con un valor predeterminado.

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser un número positivo")] // Valida que el precio sea mayor que 0.
    public decimal Price { get; set; } // Propiedad para el precio del producto.

    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")] // Valida que el stock sea igual o mayor que 0.
    public int Stock { get; set; } // Propiedad para la cantidad en stock del producto.
}
