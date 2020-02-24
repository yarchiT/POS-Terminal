using System;
using System.IO;

namespace POSTerminal.Util
{
    public static class FileReadManager
    {
        public static string[] ReadFromFile(string filePath)
        {
            string[] inputLines;

            if (File.Exists(filePath))
            {
                try
                {
                    inputLines = File.ReadAllLines(filePath);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Coudn't read input from file with path {filePath}", ex);
                }

            }
            else
            {
                throw new Exception($"File with path {filePath} doesn't exist in project directory");
            }

            return inputLines;
        }
    }
}
