using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Services
{
    public interface IServicePricesRepository
    {
        public IEnumerable<ServicePrice> GetAll();
        public ServicePrice? Get(int id);
        public bool Add(ServicePrice servicePrice);
        public bool Add(ICollection<ServicePrice> servicePrices);
        public bool CheckForDuplication(ServicePrice servicePrice);
        public IQueryable<ServicePrice> GetQueryable();

        public ICollection<KeyValuePair<int, decimal>> GetPriceByTypes(int serviceId);
    }
}
