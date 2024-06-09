using System.Text;

namespace EpilLaserLab.Server.Helpers
{
    public class DumpSaveService
    {
        const string DUMP_FOLDER = "Resources/Dumps";
        public Stream GetDump(string filename)
        {
            return File.OpenRead(GetFilePath(filename));
        }

        public string GetFilePath(string filename) => Path.Combine(DUMP_FOLDER, filename);

        public void DeleteFile(string filename)
        {
            string filePath = Path.Combine(DUMP_FOLDER, filename);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public string SaveFile(string fileBase64)
        {
            StringBuilder str = new(DateTime.Now.ToString("yyyy'_'MM'_'dd'_'HH'_'mm'_'ss"));

            str.Append($"_{new Random().Next(1000)}_dump_restore.sql");

            var name = str.ToString();
            byte[] decodedBytes = Convert.FromBase64String(fileBase64);
            File.WriteAllBytes(Path.Combine(DUMP_FOLDER, name), decodedBytes);

            return name;
        }
    }
}
