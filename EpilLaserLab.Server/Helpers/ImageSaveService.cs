using System.Text;

namespace EpilLaserLab.Server.Helpers
{
    public class ImageSaveService
    {
        const string IMAGES_FOLDER = "Resources/Images";
        public Stream GetImage(string filename)
        {
            return File.OpenRead(Path.Combine(IMAGES_FOLDER, filename));
        }

        public void DeleteFile(string filename)
        {
            string filePath = Path.Combine(IMAGES_FOLDER, filename);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public string SaveFile(string fileBase64)
        {
            StringBuilder str = new(DateTime.Now.ToString("yyyy'_'MM'_'dd'_'HH'_'mm'_'ss"));

            str.Append($"_{new Random().Next(1000)}.jpg");

            var name = str.ToString();
            byte[] decodedBytes = Convert.FromBase64String(fileBase64);
            File.WriteAllBytes(Path.Combine(IMAGES_FOLDER, name), decodedBytes);

            return name;
        }
    }
}
