using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Employees
{
    public class EmployeRepository(
        EpilLaserLabContext context) : IEmployeRepository
    {
        private readonly EpilLaserLabContext _context = context;

        public bool AccessDelete(Employee employee)
        {
            return true;
        }

        public bool Add(Employee employee)
        {
            _context.Add(employee);
            return _context.SaveChanges() > 0;
        }

        public bool CheckForDuplication(Employee employee)
        {
            return true;
        }

        public bool Delete(Employee employee)
        {

            _context.Employees.Remove(employee);
            return _context.SaveChanges() > 0;
        }

        public Employee? Get(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToArray(); 
        }

        public bool Update(Employee employeeOld, Employee employeeNew)
        {
            employeeOld.Name = employeeNew.Name;
            employeeOld.Surname = employeeNew.Surname;
            employeeOld.Patronymic = employeeNew.Patronymic;
            employeeOld.IsWork = employeeNew.IsWork;

            return _context.SaveChanges() > 0;
        }
    }
}
