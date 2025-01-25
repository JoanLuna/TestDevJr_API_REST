using System.ComponentModel.DataAnnotations; // Proporciona atributos para validación de modelos.

public class LoginRequest // Clase que representa la solicitud de inicio de sesión.
{
    [Required(ErrorMessage = "El nombre de usuario es obligatorio")] // Valida que el campo Username no sea nulo o vacío.
    public string Username { get; set; } = string.Empty; // Propiedad para el nombre de usuario con un valor predeterminado.

    [Required(ErrorMessage = "La contraseña es obligatoria")] // Valida que el campo Password no sea nulo o vacío.
    public string Password { get; set; } = string.Empty; // Propiedad para la contraseña con un valor predeterminado.
}
