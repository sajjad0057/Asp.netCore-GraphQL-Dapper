using Dapper;
using GraphQL.API.Data;
using GraphQL.API.Models;


namespace GraphQL.API.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DapperContext _context;

    public EmployeeRepository(DapperContext context) => _context = context;

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Employee>("SELECT * FROM Employees");
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM Employees WHERE Id = @Id", new { Id = id });
    }

    public async Task<Employee> CreateAsync(Employee employee)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO Employees (Name, Department, Salary) VALUES (@Name, @Department, @Salary); SELECT LAST_INSERT_ID();";
        var id = await connection.ExecuteScalarAsync<int>(sql, employee);
        employee.Id = id;
        return employee;
    }

    public async Task<Employee?> UpdateAsync(Employee employee)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE Employees SET Name = @Name, Department = @Department, Salary = @Salary WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(sql, employee);
        return rowsAffected > 0 ? employee : null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Employees WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
        return rowsAffected > 0;
    }
}