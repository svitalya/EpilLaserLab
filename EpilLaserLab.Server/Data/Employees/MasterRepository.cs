using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EpilLaserLab.Server.Data.Employees
{
    public class MasterRepository(
        EpilLaserContext context,
        IEmployeRepository employeRepository
        ) : IMasterRepository
    {

        EpilLaserContext _context = context;

        public bool AccessDelete(Master master)
        {
            return master.MasterId % 2 == 0;
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
