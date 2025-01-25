using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using Microsoft.IdentityModel.Tokens; // Proporciona clases para manejar seguridad y tokens JWT.
using System.IdentityModel.Tokens.Jwt; // Proporciona clases para generar y manejar tokens JWT.
using System.Security.Claims; // Incluye clases para manejar los claims dentro de un token JWT.
using System.Text;
using MyApi.Data;

[ApiController]
[Route("api/[controller]")] 
public class AuthController : ControllerBase // Define un controlador base para manejar solicitudes HTTP relacionadas con autenticación.
{
    private readonly ProductDbContext _context; // Campo para interactuar con la base de datos a través del contexto.
    private readonly IConfiguration _configuration; // Proporciona acceso a la configuración de la aplicación, como claves JWT.

    public AuthController(ProductDbContext context, IConfiguration configuration) // Constructor que recibe el contexto de base de datos y configuración como dependencias.
    {
        _context = context;
        _configuration = configuration;
    }

    // Registro de usuario
    [HttpPost("register")] // Indica que este método maneja solicitudes HTTP POST para registrar usuarios.
    public async Task<IActionResult> Register([FromBody] User user) // Recibe los datos del usuario desde el cuerpo de la solicitud.
    {
        if (!ModelState.IsValid) // Verifica si el modelo recibido es válido.
        {
            return BadRequest(new { Message = "Datos inválidos", Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) }); // Responde con un error HTTP 400 si hay errores en la validación.
        }

        // Verificar si el usuario o correo ya existe
        if (await _context.Users.AnyAsync(u => u.Username == user.Username || u.Email == user.Email)) // Comprueba si el usuario o correo ya están registrados.
        {
            return BadRequest(new { Message = "El usuario o correo ya está registrado" }); // Responde con un error HTTP 400 si ya existe.
        }

        // Hashear la contraseña
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); // Cifra la contraseña antes de guardarla.

        // Guardar el usuario en la base de datos
        _context.Users.Add(user); // Agrega el nuevo usuario al contexto de base de datos.
        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos.

        return Ok(new { Message = "Usuario registrado exitosamente" }); // Responde con un mensaje de éxito.
    }

    [HttpPost("login")] // Indica que este método maneja solicitudes HTTP POST para login.
    public async Task<IActionResult> Login([FromBody] LoginRequest credentials) // Recibe las credenciales de inicio de sesión desde el cuerpo de la solicitud.
    {
        if (!ModelState.IsValid) // Verifica si el modelo recibido es válido.
        {
            return BadRequest(new { Message = "Datos inválidos", Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) }); // Responde con un error HTTP 400 si hay errores en la validación.
        }

        // Log para verificar el username recibido
        Console.WriteLine($"Intentando iniciar sesión con Username: {credentials.Username}");

        // Buscar el usuario en la base de datos
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == credentials.Username); // Busca el usuario por nombre de usuario.

        if (user == null) // Si no encuentra el usuario.
        {
            Console.WriteLine("Usuario no encontrado");
            return Unauthorized(new { Message = "Credenciales inválidas" }); // Responde con un error HTTP 401 si las credenciales son incorrectas.
        }

        // Verificar la contraseña
        if (!BCrypt.Net.BCrypt.Verify(credentials.Password, user.Password)) // Comprueba si la contraseña es incorrecta.
        {
            Console.WriteLine("Contraseña incorrecta");
            return Unauthorized(new { Message = "Credenciales inválidas" }); // Responde con un error HTTP 401 si la contraseña es incorrecta.
        }

        Console.WriteLine("Inicio de sesión exitoso");
        var token = GenerateJwtToken(user); // Genera un token JWT para el usuario autenticado.

        return Ok(new { Message = "Login exitoso", Token = token }); // Responde con un mensaje de éxito y el token generado.
    }

    private string GenerateJwtToken(User user) // Método para generar un token JWT.
    {
        var claims = new[] // Define los claims que se incluirán en el token.
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username), // Claim: Sub (Subject) con el nombre de usuario.
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Claim: Jti (Token ID) único para el token.
            new Claim(ClaimTypes.Name, user.Username) // Claim: Name con el nombre de usuario.
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "SuperSecretKey@345")); // Clave secreta para firmar el token.
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Define el algoritmo de firma del token.

        var token = new JwtSecurityToken( // Crea un nuevo token JWT.
            issuer: _configuration["Jwt:Issuer"] ?? "yourapi.com", // Define el emisor del token.
            audience: _configuration["Jwt:Audience"] ?? "yourapi.com", // Define la audiencia del token.
            claims: claims, // Incluye los claims definidos anteriormente.
            expires: DateTime.Now.AddHours(1), // Establece la expiración del token.
            signingCredentials: creds // Firma el token con las credenciales generadas.
        );

        return new JwtSecurityTokenHandler().WriteToken(token); // Convierte el token a una cadena y lo devuelve.
    }
}
