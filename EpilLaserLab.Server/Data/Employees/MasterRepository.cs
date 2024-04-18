using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EpilLaserLab.Server.Data.Employees
{
    public class MasterRepository(
        EpilLaserLabContext context,
        IEmployeRepository employeRepository
        ) : IMasterRepository
    {

        EpilLaserLabContext _context = context;

        public bool AccessDelete(Master master)
        {
            CollectionEntry<Master, Schedule> schedules = _context
                .Entry(master)
                .Collection(m => m.Schedules);

            if (!schedules.IsLoaded)
            {
                schedules.Load();
            }

            return !master.Schedules.Any();
        }

        public bool Add(Master master)
        {
            _context.Masters.Add( master );
            return _context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(Master master)
        {
            return true;
        }

        public bool Delete(Master master)
        {
            _context.Masters.Remove(master);
            return _context.SaveChanges() > 0;
        }

        public Master? Get(int id)
        {
            return GetQueryable().FirstOrDefault(m => m.MasterId == id);
        }

        public IEnumerable<Master> GetAll()
        {
            return GetQueryable().ToArray();
        }

        public IQueryable<Master> GetQueryable()
        {
            return _context.Masters
                .Include(m => m.Employee)
                .Include(m => m.Branch)
                .AsQueryable();
        }

        public bool Update(Master masterOld, Master masterNew)
        {
            masterOld.PhotoPath = masterNew.PhotoPath;
            masterOld.BranchId = masterNew.BranchId;

            return _context.SaveChanges() > 0;
        }
    }
}
