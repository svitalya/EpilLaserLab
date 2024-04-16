using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EpilLaserLab.Server.Data.Branches
{
    public interface IBranchRepository
    {
        public IEnumerable<Branch> GetAll();
        public Branch? Get(int id);
        public bool Add(Branch branch);
        public bool Update(Branch branchOld,Branch branchNew);
        public bool Delete(Branch branch);
        public bool CheckForDuplication(Branch branch);
        public bool AccessDelete(Branch branch);
        public IQueryable<Branch> GetQuerable();
    }
}

