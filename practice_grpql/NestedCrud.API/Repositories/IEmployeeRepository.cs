using NestedCrud.API.Models;

namespace NestedCrud.API.Repositories
{
    public interface IEmployeeRepository
    {
        Task<int> CreateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<bool> UpdateEmployeeAsync(Employee employee);
    }
}