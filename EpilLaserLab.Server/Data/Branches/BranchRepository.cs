using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EpilLaserLab.Server.Data.Branches
{
    public class BranchRepository(
            EpilLaserContext context
        )
        : IBranchRepository
    {

        private readonly EpilLaserContext _context = context;

        public bool AccessDelete(Branch branch)
        {
            CollectionEntry<Branch, Master> masters = _context
                .Entry(branch)
                .Collection(s => s.Masters);

            if (!masters.IsLoaded)
            {
                masters.Load();
            }

            return !branch.Masters.Any();
        }

        public bool Add(Branch branch)
        {
            _context.Branches.Add(branch);
            return _context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(Branch branch)
        {
            return !_context.Branches
                .Where(b => b.Address.Equals(branch.Address,StringComparison.CurrentCultureIgnoreCase)
                    && b.BranchId != branch.BranchId)
                .Any();
        }

        public bool Delete(Branch branch)
        {
            _context.Branches.Remove(branch);
            return _context.SaveChanges() > 0;
        }

        public Branch? Get(int id)
        {
            return _context.Branches
                .FirstOrDefault(b => b.BranchId == id);
        }

        public IEnumerable<Branch> GetAll()
        {
            return _context.Branches.ToArray();
        }

        public IQueryable<Branch> GetQuerable()
        {
            return _context.Branches.AsQueryable();
        }

        public bool Update(Branch branchOld, Branch branchNew)
        {
            branchOld.Address = branchNew.Address;

            if(branchNew.PhotoPath != string.Empty)
            {
                branchOld.PhotoPath = branchNew.PhotoPath;
            }

            return _context.SaveChanges() > 0;
        }
    }
}

