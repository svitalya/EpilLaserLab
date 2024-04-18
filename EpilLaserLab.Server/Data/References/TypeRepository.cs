
using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Type = EpilLaserLab.Server.Models.Type;

namespace EpilLaserLab.Server.Data.References
{
    public class TypeRepository : ITypeRepository
    {
        private readonly EpilLaserLabContext _context;

        public TypeRepository(EpilLaserLabContext context)
        {
            this._context = context;
        }

        public bool AccessDelete(Type type)
        {
            CollectionEntry<Type, ServicePrice> servicePrices = _context
                .Entry(type)
                .Collection(s => s.ServicePrices);

            if (!servicePrices.IsLoaded)
            {
                servicePrices.Load();
            }

            return !type.ServicePrices.Any();
        }

        public bool Add(Models.Type type)
        {
            _context.Types.Add(type);
            return _context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(Models.Type type)
        {
            return !_context.Types.Where(c => c.Name.Equals(type.Name, StringComparison.CurrentCultureIgnoreCase)).Any();
        }

        public bool Delete(Models.Type type)
        {
            _context.Types.Remove(type);
            return _context.SaveChanges() > 0;
        }

        public Models.Type? Get(int id)
        {
            return _context.Types.FirstOrDefault(c => c.TypeId == id);
        }

        public IEnumerable<Models.Type> GetAll()
        {
            return _context.Types.ToArray();
        }

        public bool Update(Models.Type typeOld, Models.Type typeNew)
        {
            typeOld.Name = typeNew.Name;

            return _context.SaveChanges() > 0;
        }
    }
}
