using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Authentication.Controllers;

[Route("/health")]
[ApiController]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult CheckHealth()
    {
        // Aquí puedes realizar cualquier verificación de salud necesaria
        // Por ejemplo, puedes verificar la conexión a la base de datos, servicios externos, etc.
        // Devuelve un código de estado 200 si el servicio está en buen estado, de lo contrario, un código de error 500
        bool isHealthy = true; // Aquí implementa tu lógica de verificación de salud
        return isHealthy ? Ok("Service is healthy") : StatusCode(500, "Service is unhealthy");
    }
}