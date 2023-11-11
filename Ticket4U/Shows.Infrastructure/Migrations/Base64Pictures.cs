namespace Shows.Infrastructure.Migrations;

public static class Base64Pictures
{
    public static string ReadFromFile(string fileName)
    {
        string relativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

        try
        {
            if (File.Exists(relativePath))
            {
                return File.ReadAllText(relativePath);
            }
            else
            {
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }
}
