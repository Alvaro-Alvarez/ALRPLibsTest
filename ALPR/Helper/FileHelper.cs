namespace ALPR.Helper
{
    public static class FileHelper
    {
        public static List<string> GetFiles(string path, string extensionsStr)
        {
            var files = new List<string>();
            var extensions = extensionsStr.Split(',');
            try
            {
                if (Directory.Exists(path))
                {
                    string[] filesInDirectory = Directory.GetFiles(path);
                    foreach (string file in filesInDirectory)
                    {
                        string extension = Path.GetExtension(file);
                        if (extensions.Contains(extension))
                            files.Add(file);
                    }
                }
                else Console.WriteLine("El directorio no existe.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return files;
        }
    }
}
