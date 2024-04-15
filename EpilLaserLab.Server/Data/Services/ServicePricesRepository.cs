using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Services
{
    public class ServicePricesRepository : IServicePricesRepository
    {
        private readonly EpilLaserContext _context;

        public ServicePricesRepository(EpilLaserContext context)
        {
            _context = context;
        }

        public bool Add(ServicePrice servicePrice)
        {
            _context.ServicePrices.Add(servicePrice);
            return _context.SaveChanges() > 0;
        }

        public bool Add(ICollection<ServicePrice> servicePrices)
        {
            _context.ServicePrices.AddRange(servicePrices);
            return _context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(ServicePrice servicePrice)
        {
            return !GetPriceByTypes(servicePrice.ServiceId)
                .Where(g => g.Key == servicePrice.TypeId && g.Value == servicePrice.Price)
                .Any();
        }

        public ServicePrice? Get(int id)
        {
            return _context.ServicePrices.FirstOrDefault(c => c.ServicePriceId == id);
        }

        public IEnumerable<ServicePrice> GetAll()
        {
            return _context.ServicePrices.ToArray();
        }

        public ICollection<KeyValuePair<int, decimal>> GetPriceByTypes(int serviceId)
        {
            return _context.ServicePrices
                .Where(sp => sp.ServiceId == serviceId)
                .GroupBy(sp => sp.TypeId)
                .Select(g => new KeyValuePair<int, decimal>(
                    g.Key,
                    g.OrderByDescending(sp => sp.DateTime).FirstOrDefault()!.Price
                ))
                .ToArray();

        }

        public IQueryable<ServicePrice> GetQueryable()
        {
            return _context.ServicePrices.AsQueryable();
        }
    }
}
