using Dapper;
using NestedCrud.API.Data;
using NestedCrud.API.Models;

namespace NestedCrud.API.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DapperContext _context;

    public EmployeeRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
        var query = "SELECT * FROM Employees";
        using var connection = _context.CreateConnection();
        var employees = await connection.QueryAsync<Employee>(query);

        foreach (var emp in employees)
        {
            emp.Address = await connection.QueryFirstOrDefaultAsync<Address>(
                "SELECT * FROM Addresses WHERE EmployeeId = @Id", new { emp.Id }) ?? new Address();

            var departmentQuery = @"SELECT d.* FROM Departments d 
                                    JOIN EmployeeDepartments ed ON d.Id = ed.DepartmentId 
                                    WHERE ed.EmployeeId = @Id";

            emp.Departments = (await connection.QueryAsync<Department>(departmentQuery, new { emp.Id })).ToList() ?? new List<Department>();
        }

        return employees;
    }

    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
        var query = "SELECT * FROM Employees WHERE Id = @Id";
        using var connection = _context.CreateConnection();
        var employee = await connection.QueryFirstOrDefaultAsync<Employee>(query, new { id });

        if (employee == null) return null;

        employee.Address = await connection.QueryFirstOrDefaultAsync<Address>(
            "SELECT * FROM Addresses WHERE EmployeeId = @Id", new { id }) ?? new Address();

        var departmentQuery = @"SELECT d.* FROM Departments d 
                                JOIN EmployeeDepartments ed ON d.Id = ed.DepartmentId 
                                WHERE ed.EmployeeId = @Id";
        employee.Departments = (await connection.QueryAsync<Department>(departmentQuery, new { id })).ToList() ?? new List<Department>();

        return employee;
    }

    public async Task<int> CreateEmployeeAsync(Employee employee)
    {
        var query = "INSERT INTO Employees (Name, Email) VALUES (@Name, @Email); SELECT LAST_INSERT_ID();";
        using var connection = _context.CreateConnection();
        var employeeId = await connection.ExecuteScalarAsync<int>(query, employee);

        if (employee.Address != null)
        {
            employee.Address.EmployeeId = employeeId;
            var addressQuery = @"INSERT INTO Addresses (Street, City, Country, EmployeeId)
                                 VALUES (@Street, @City, @Country, @EmployeeId)";
            await connection.ExecuteAsync(addressQuery, employee.Address);
        }

        if (employee.Departments != null)
        {
            foreach (var dept in employee.Departments)
            {
                var relationQuery = @"INSERT INTO EmployeeDepartments (EmployeeId, DepartmentId) 
                                      VALUES (@EmployeeId, @DepartmentId)";
                await connection.ExecuteAsync(relationQuery, new { EmployeeId = employeeId, DepartmentId = dept.Id });
            }
        }

        return employeeId;
    }

    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        var query = "UPDATE Employees SET Name = @Name, Email = @Email WHERE Id = @Id";
        using var connection = _context.CreateConnection();
        var result = await connection.ExecuteAsync(query, employee);

        if (employee.Address != null)
        {
            var updateAddress = @"UPDATE Addresses SET Street = @Street, City = @City, Country = @Country 
                                  WHERE EmployeeId = @EmployeeId";
            await connection.ExecuteAsync(updateAddress, employee.Address);
        }

        // Remove old relations
        await connection.ExecuteAsync("DELETE FROM EmployeeDepartments WHERE EmployeeId = @Id", new { employee.Id });

        if (employee.Departments != null)
        {
            foreach (var dept in employee.Departments)
            {
                await connection.ExecuteAsync(@"INSERT INTO EmployeeDepartments (EmployeeId, DepartmentId) 
                                                VALUES (@EmployeeId, @DepartmentId)",
                                                new { EmployeeId = employee.Id, DepartmentId = dept.Id });
            }
        }

        return result > 0;
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        var query = "DELETE FROM Employees WHERE Id = @Id";
        using var connection = _context.CreateConnection();
        var result = await connection.ExecuteAsync(query, new { id });
        return result > 0;
    }
}
