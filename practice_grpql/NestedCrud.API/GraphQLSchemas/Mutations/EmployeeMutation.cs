using NestedCrud.API.Models;
using NestedCrud.API.Models.Input;
using NestedCrud.API.Repositories;

namespace NestedCrud.API.GraphQLSchemas.Mutations;

public class EmployeeMutation
{
    public async Task<Employee> CreateEmployee(CreateEmployeeInput employee, [Service] IEmployeeRepository repo)
    {
        if (employee.Address == null)
        {
            throw new ArgumentNullException(nameof(employee.Address), "Address cannot be null.");
        }

        var employeeModel = new Employee
        {
            Name = employee.Name,
            Email = employee.Email,

            Address = new Address
            {
                Street = employee.Address.Street,
                City = employee.Address.City,
                Country = employee.Address.Country
            },

            Departments = employee.Departments?.Select(d => new Department { Id = d.Id }).ToList() ?? new List<Department>()
        };

        var id = await repo.CreateEmployeeAsync(employeeModel);

        return await repo.GetEmployeeByIdAsync(id);
    }

    public async Task<bool> UpdateEmployee(UpdateEmployeeInput employee, [Service] IEmployeeRepository repo)
    {
        var employeeModel = new Employee
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            Address = employee.Address == null ? null : new Address
            {
                Street = employee.Address.Street,
                City = employee.Address.City,
                Country = employee.Address.Country,
                EmployeeId = employee.Id
            },
            Departments = employee.Departments?.Select(d => new Department { Id = d.Id }).ToList()
        };

        return await repo.UpdateEmployeeAsync(employeeModel);
    }


    public async Task<bool> DeleteEmployee(int id, [Service] IEmployeeRepository repo)
        => await repo.DeleteEmployeeAsync(id);
}

