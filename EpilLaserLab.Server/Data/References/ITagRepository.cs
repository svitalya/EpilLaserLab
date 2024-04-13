using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.References
{
    public interface ITagRepository
    {
        public IEnumerable<Tag> GetAll();
        public Tag? Get(int id);
        public bool Add(Tag tag);
        public bool Update(Tag tagOld, Tag tagNew);
        public bool Delete(Tag tag);
        public bool CheckForDuplication(Tag tag);
        public bool AccessDelete(Tag tag);
    }
}
