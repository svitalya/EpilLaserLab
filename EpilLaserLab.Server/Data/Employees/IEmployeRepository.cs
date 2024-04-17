using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Employees
{
    public interface IEmployeRepository
    {
        public IEnumerable<Employee> GetAll();
        public Employee? Get(int id);
        public bool Add(Employee employee);
        public bool Update(Employee employeeOld, Employee employeeNew);
        public bool Delete(Employee employee);
        public bool CheckForDuplication(Employee employee);
        public bool AccessDelete(Employee employee);
    }
}
