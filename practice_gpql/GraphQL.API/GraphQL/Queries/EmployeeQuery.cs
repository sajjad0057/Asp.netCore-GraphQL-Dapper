using GraphQL.API.Models;
using GraphQL.API.Repositories;

namespace GraphQL.API.GraphQL.Queries;

public class EmployeeQuery
{
    public Task<IEnumerable<Employee>> GetEmployees([Service] IEmployeeRepository repo) => repo.GetAllAsync();

    public Task<Employee?> GetEmployeeById(int id, [Service] IEmployeeRepository repo) => repo.GetByIdAsync(id);
}