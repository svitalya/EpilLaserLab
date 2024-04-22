using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Helpers
{
    public class TestDocumentService
    {

        const string IMAGES_FOLDER = "Resources/Docs";

        internal Stream GetDocument(ICollection<Application> applications)
        {
            return File.OpenRead(Path.Combine(IMAGES_FOLDER, "test.pdf"));
        }
    }
}
