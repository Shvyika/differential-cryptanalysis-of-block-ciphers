using System.Text;

namespace differential_cryptanalysis
{
    internal class FileSystem
    {
        public static string ReadFromBinaryFile(string fileName)
        {
            string content;
     
            using var stream = File.Open(fileName, FileMode.Open);

            using var reader = new BinaryReader(stream, Encoding.UTF8, false);

            content = reader.ReadString();

            return content;
        }

        public static void WriteToBinaryFile(string fileName, string content)
        {
            using var stream = File.Open(fileName, FileMode.Open);

            using var writer = new BinaryWriter(stream, Encoding.UTF8, false);

            writer.Write(content);
        }
    }
}
