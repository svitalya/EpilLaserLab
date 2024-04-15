using EpilLaserLab.Server.Models;
using Type = EpilLaserLab.Server.Models.Type;

namespace EpilLaserLab.Server.Data.Tables
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
