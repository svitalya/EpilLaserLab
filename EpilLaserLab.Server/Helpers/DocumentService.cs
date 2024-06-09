using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Helpers
{
    public class DocumentService
    {

        const string DOCS_FOLDER = "Resources/Docs";

        public Stream GetDocument(string name)
        {
            return File.OpenRead(Path.Combine(DOCS_FOLDER, name));
        }

        public string CreateDocument(string documentType, object data)
        {
            throw new NotImplementedException();
        }
    }
}
