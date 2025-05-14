using NestedCrud.API.Models;
using NestedCrud.API.Repositories;

namespace NestedCrud.API.GraphQLSchemas.Queries;

public class EmployeeQuery
{
    public async Task<IEnumerable<Employee>> GetEmployees([Service] IEmployeeRepository repo)
    => await repo.GetEmployeesAsync();

    public async Task<Employee> GetEmployeeById(int id, [Service] IEmployeeRepository repo)
        => await repo.GetEmployeeByIdAsync(id);
}

