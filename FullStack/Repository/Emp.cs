using FullStack.Data;
using FullStack.Models;

namespace FullStack.Repository
{
    public class Emp : IEmp
    {
        private readonly FullStackDbContext fullStackDbContext;
        public Emp(FullStackDbContext fullStackDbContext)
        {
           
            this.fullStackDbContext = fullStackDbContext;
        }

        public Employee DeleteEmployee(int empID)
        {
            var emp = fullStackDbContext.EmpTabs.FirstOrDefault(x => x.EmpID == empID);
            fullStackDbContext.EmpTabs.Remove(emp);
            fullStackDbContext.SaveChanges();
            return emp;
        }

        public Employee GetEmployeebyID(int EmpID)
        {
            var emp = fullStackDbContext.EmpTabs.First(x => x.EmpID == EmpID);
            return emp;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            var employees = fullStackDbContext.EmpTabs.ToList();
            return employees;
        }
    }
}
