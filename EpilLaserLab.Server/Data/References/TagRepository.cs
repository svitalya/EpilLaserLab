using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.References
{
    public class TagRepository : ITagRepository
    {
        private readonly EpilLaserContext _context;

        public TagRepository(EpilLaserContext context)
        {
            _context = context;
        }

        public bool AccessDelete(Tag tag)
        {
            return tag.TagId % 2 == 0;
        }

        public bool Add(Tag tag)
        {
            _context.Tags.Add(tag);
            return _context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(Tag tag)
        {
            return !_context.Tags.Where(t => t.Name.Equals(tag.Name, StringComparison.CurrentCultureIgnoreCase)).Any();
        }

        public bool Delete(Tag tag)
        {
            _context.Tags.Remove(tag);
            return _context.SaveChanges() > 0;
        }

        public Tag? Get(int id)
        {
            return _context.Tags.FirstOrDefault(t => t.TagId == id);
        }

        public IEnumerable<Tag> GetAll()
        {
            return _context.Tags.ToArray();
        }

        public bool Update(Tag tagOld, Tag tagNew)
        {
            tagOld.Name = tagNew.Name;

            return _context.SaveChanges() > 0;
        }
    }
}
