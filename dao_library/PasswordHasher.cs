namespace dao_library;

/// <summary>
/// Hasheo y verificación de contraseñas con BCrypt (cost 11).
/// Acepta hashes $2a$ y $2b$.
/// </summary>
public static class PasswordHasher
{
    private const int WorkFactor = 11;

    public static string Hash(string plainPassword)
    {
        return BCrypt.Net.BCrypt.HashPassword(plainPassword, WorkFactor);
    }

    public static bool Verify(string plainPassword, string hash)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hash);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Detecta si el valor ya es un hash BCrypt (para no re-hashear).
    /// </summary>
    public static bool IsHashed(string value)
    {
        return value.StartsWith("$2a$") || value.StartsWith("$2b$") || value.StartsWith("$2y$");
    }
}
