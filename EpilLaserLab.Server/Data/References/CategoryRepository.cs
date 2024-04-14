using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.References
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EpilLaserContext _context;

        public CategoryRepository(EpilLaserContext context)
        {
            _context = context;
        }

        public bool AccessDelete(Category category)
        {
            return category.CategoryId % 2 == 0;
        }

        public bool Add(Category category)
        {
            _context.Categories.Add(category);
            return _context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(Category category)
        {
            return !_context.Categories.Where(c => c.Name.Equals(category.Name, StringComparison.CurrentCultureIgnoreCase)).Any();
        }

        public bool Delete(Category category)
        {
            _context.Categories.Remove(category);
            return _context.SaveChanges() > 0;
        }

        public Category? Get(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToArray();
        }

        public bool Update(Category categoryOld, Category categoryNew)
        {
            categoryOld.Name = categoryNew.Name;

            return _context.SaveChanges() > 0;
        }
    }

}
