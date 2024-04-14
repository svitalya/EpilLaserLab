using EpilLaserLab.Server.Models;

using Type = EpilLaserLab.Server.Models.Type;

namespace EpilLaserLab.Server.Data.References
{
    public interface ITypeRepository
    {
        public IEnumerable<Type> GetAll();
        public Type? Get(int id);
        public bool Add(Type type);
        public bool Update(Type typeOld, Type typeNew);
        public bool Delete(Type type);
        public bool CheckForDuplication(Type type);
        public bool AccessDelete(Type type);
    }
}
