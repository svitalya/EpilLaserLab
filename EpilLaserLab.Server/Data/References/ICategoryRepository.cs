using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.References
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetAll();
        public Category? Get(int id);
        public bool Add(Category category);
        public bool Update(Category categoryOld, Category categoryNew);
        public bool Delete(Category category);
        public bool CheckForDuplication(Category category);
        public bool AccessDelete(Category category);
    }
}
