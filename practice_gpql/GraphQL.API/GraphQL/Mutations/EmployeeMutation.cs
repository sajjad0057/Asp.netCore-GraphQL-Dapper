using GraphQL.API.DTOs;
using GraphQL.API.Models;
using GraphQL.API.Repositories;

namespace GraphQL.API.GraphQL.Mutations;


public class EmployeeMutation
{
    public Task<Employee> CreateEmployee(CreateEmpoyeeInput input, [Service] IEmployeeRepository repo)
    {
        var employee = new Employee
        {
            Name = input.Name,
            Department = input.Department,
            Salary = input.Salary
        };

        return repo.CreateAsync(employee);
    }

    public Task<Employee?> UpdateEmployee(Employee input, [Service] IEmployeeRepository repo) => repo.UpdateAsync(input);

    public Task<bool> DeleteEmployee(int id, [Service] IEmployeeRepository repo) => repo.DeleteAsync(id);
}