using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.References
{
    public class StatusRepository : IStatusRepository
    {
        private readonly EpilLaserLabContext _context;

        public StatusRepository(EpilLaserLabContext context) {
            _context = context;
        }

        public bool AccessDelete(Status status)
        {
            return status.StatusId % 2 == 0; 
        }

        public bool Add(Status status)
        {
            _context.Statuses.Add(status);
            return _context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(Status status)
        {
            return !_context.Statuses.Where(t => t.Name.Equals(status.Name, StringComparison.CurrentCultureIgnoreCase)).Any();
        }

        public bool Delete(Status status)
        {
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

        public bool Update(Status statusOld, Status statusNew)
        {
            statusOld.Name = statusNew.Name;

            return _context.SaveChanges() > 0;
        }
    }
}
