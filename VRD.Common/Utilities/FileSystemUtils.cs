using System.IO;

namespace VRD.Common.Utilities
{
    /// <summary>
    /// Provides various utility methods to work with file system.
    /// </summary>
    public class FileSystemUtils
    {
        /// <summary>
        /// Ensures the given directory exists by recursively creating the root directories.
        /// </summary>
        /// <param name="path"> Path to the directory. </param>
        /// <returns> Returns <c>true</c> if the <paramref name="path" /> exists or is successfully created, <c>false</c> otherwise. </returns>
        public static bool EnsureDirectoryExists(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }

            var parent = Path.GetDirectoryName(path);

            if (string.IsNullOrWhiteSpace(parent))
            {
                Directory.CreateDirectory(path);
                return true;
            }

            if (EnsureDirectoryExists(parent))
            {
                Directory.CreateDirectory(path);
                return true;
            }

            return false;
        }
    }
}
