using System.IO;
using System.Runtime.Serialization;
using VRD.Common.Utilities;

namespace VRD.Common.Tools
{
    public class VrdDataContractSerialzer<T>
    {
        /// <summary>
        /// Serializes the given object to a file.
        /// </summary>
        /// <param name="data"> The object to serialize. </param>
        /// <param name="fileName"> Name of file where to serialize. The file must not exist. </param>
        public void SerializeToFile(T data, string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            FileSystemUtils.EnsureDirectoryExists(Path.GetDirectoryName(fileName));

            if (GenericUtils.IsNull(data))
            {
                File.WriteAllText(fileName, string.Empty);

                return;
            }

            using (var fileStream = File.OpenWrite(fileName))
            {
                var dataContractSerializer = new DataContractSerializer(typeof(T));

                dataContractSerializer.WriteObject(fileStream, data);
            }
        }

        /// <summary>
        /// Restores <see cref="T" /> by deserializing the given file.
        /// </summary>
        /// <param name="fileName"> Name of the file to deserialize. The file must exist. </param>
        /// <returns> Returns <typeparamref name="T" /> as deserialized or default. </returns>
        public T DeserializeFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return GenericUtils.CreateDefaultInstance<T>();
            }

            using (var fileStream = File.OpenRead(fileName))
            {
                var dataContractSerializer = new DataContractSerializer(typeof(T));

                return (T)dataContractSerializer.ReadObject(fileStream);
            }
        }
    }
}
