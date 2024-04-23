using System.Security.Cryptography;

namespace AuthenticationService.Shared.Hashing;

public class PasswordVerifier
{
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        // Obtener el salt y el hash almacenados
        byte[] hashWithSaltBytes = Convert.FromBase64String(hashedPassword);
        byte[] salt = new byte[16];
        Array.Copy(hashWithSaltBytes, 0, salt, 0, salt.Length);

        // Generar el hash de la contraseña ingresada utilizando el mismo salt y parámetros
        int iterations = 10000;
        int keyLength = 32;
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
        {
            byte[] hashBytes = pbkdf2.GetBytes(keyLength);

            // Comparar el hash generado con el hash almacenado
            for (int i = 0; i < hashBytes.Length; i++)
            {
                if (hashBytes[i] != hashWithSaltBytes[salt.Length + i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
