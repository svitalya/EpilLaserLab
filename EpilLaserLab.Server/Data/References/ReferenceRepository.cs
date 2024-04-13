using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.References
{
    public class StatusRepository : IStatusRepository
    {
        private readonly EpilLaserContext _context;

        public StatusRepository(EpilLaserContext context) {
            _context = context;
        }

        public bool Add(Status status)
        {
            _context.Statuses.Add(status);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var status = _context.Statuses.Find(id);
            if (status is null) return false;

            _context.Statuses.Remove(status);
            return _context.SaveChanges() > 0;
        }

        public Status? Get(int id)
        {
            return _context.Statuses.FirstOrDefault(s => s.StatusId == id);
        }

        public IEnumerable<Status> GetAll()
        {
            return _context.Statuses.ToArray();
        }

        public bool Update(int id, Status status)
        {
            var statusOld = _context.Statuses.Find(id);
            if (statusOld is null) return false;

            statusOld.Name = status.Name;

            return _context.SaveChanges() > 0;
        }
    }
}
