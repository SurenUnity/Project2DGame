using System.IO;
using UnityEngine;

namespace Utils
{
    public static class PersistentPathUtility
    {
        public static string GetFilePath(string filePath)
        {
            return Path.Combine(Application.persistentDataPath, filePath);
        }
        
        public static string GetFilePath(string filePath, string ext)
        {
            var path = Path.Combine(Application.persistentDataPath, filePath);
            return $"{path}.{ext}";
        }
        
        public static bool TryCreateDirectory(string directoryName)
        {
            var dirPath = Path.Combine(Application.persistentDataPath, directoryName);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                return true;
            }

            return false;
        }
    }
}