using FullStack.Controllers;
using FullStack.Models;

namespace FullStack.Repository
{
    public interface IEmp
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeebyID(int EmpID);
        Employee DeleteEmployee(int empID);
    }
}
