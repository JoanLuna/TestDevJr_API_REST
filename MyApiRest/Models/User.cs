using System.ComponentModel.DataAnnotations; // Proporciona atributos para la validación de modelos.

public class User // Clase que representa al modelo de usuario.
{
    public int Id { get; set; } // Clave primaria automática para identificar al usuario.

    [Required(ErrorMessage = "El nombre de usuario es obligatorio")] // Valida que el campo Username no sea nulo o vacío.
    [StringLength(50, ErrorMessage = "El nombre de usuario no debe exceder los 50 caracteres")] // Restringe la longitud máxima del nombre de usuario.
    public string Username { get; set; } = string.Empty; // Propiedad para el nombre de usuario con un valor predeterminado.

    [Required(ErrorMessage = "El correo electrónico es obligatorio")] // Valida que el campo Email no sea nulo o vacío.
    [EmailAddress(ErrorMessage = "Correo inválido")] // Valida que el valor sea un formato válido de correo electrónico.
    public string Email { get; set; } = string.Empty; // Propiedad para el correo electrónico con un valor predeterminado.

    [Required(ErrorMessage = "La contraseña es obligatoria")] // Valida que el campo Password no sea nulo o vacío.
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres")] // Restringe la longitud de la contraseña a un rango válido.
    public string Password { get; set; } = string.Empty; // Propiedad para la contraseña hasheada del usuario.
}
