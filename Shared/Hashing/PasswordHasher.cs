using System.Security.Cryptography;

namespace AuthenticationService.Shared.Hashing;

public class PasswordHasher
{
    public static string HashPassword(string password)
    {
        // Generar un salt aleatorio
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Generar el hash utilizando PBKDF2 con un número de iteraciones y longitud de clave
        int iterations = 10000; // Número de iteraciones recomendado por la NIST
        int keyLength = 32; // Longitud de la clave en bytes
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
        {
            byte[] hashBytes = pbkdf2.GetBytes(keyLength);

            // Combinar el salt y el hash para almacenarlos juntos
            byte[] hashWithSaltBytes = new byte[salt.Length + hashBytes.Length];
            Array.Copy(salt, 0, hashWithSaltBytes, 0, salt.Length);
            Array.Copy(hashBytes, 0, hashWithSaltBytes, salt.Length, hashBytes.Length);

            return Convert.ToBase64String(hashWithSaltBytes);
        }
    }
}