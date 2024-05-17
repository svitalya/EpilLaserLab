using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EpilLaserLab.Server.Data.Services
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly EpilLaserLabContext _context;

        public ServiceRepository(EpilLaserLabContext context)
        {
            _context = context;
        }

        public bool AccessDelete(Service service)
        {
            CollectionEntry<Service, ServicePrice> servicePrices = _context
                .Entry(service)
                .Collection(s => s.ServicePrices);

            if (!servicePrices.IsLoaded)
            {
                servicePrices.Load();
            }

            return !service.ServicePrices.Any();
        }

        public bool Add(Service service)
        {
            _context.Services.Add(service);
            return _context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(Service service)
        {
            return !_context.Services
                .Where(s => s.Name.Equals(service.Name, StringComparison.CurrentCultureIgnoreCase) && s.ServiceId != service.ServiceId)
                .Any();
        }

        public bool Delete(Service service)
        {
            _context.Services.Remove(service);
            return _context.SaveChanges() > 0;
        }

        public Service? Get(int id)
        {
            return _context.Services.FirstOrDefault(c => c.ServiceId == id);
        }

        public IEnumerable<Service> GetAll()
        {
            return _context.Services.ToArray();
        }

        public ServicePrice? GetPriceByType(Service service, int typeId)
        {
            return _context.ServicePrices
                .Where(sp => sp.ServiceId == service.ServiceId && sp.TypeId == typeId)
                .OrderByDescending(sp => sp.DateTime)
                .FirstOrDefault();

        }

        public IQueryable<Service> GetQuerable()
        {
            return _context.Services.AsQueryable();
        }

        public bool Update(Service serviceOld, Service serviceNew)
        {
            serviceOld.Name = serviceNew.Name;
            serviceOld.Description = serviceNew.Description;
            serviceOld.TimeCost = serviceNew.TimeCost;

            return _context.SaveChanges() > 0;
        }
    }
}
