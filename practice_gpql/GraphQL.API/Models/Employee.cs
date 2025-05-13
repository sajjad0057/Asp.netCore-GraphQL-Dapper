namespace GraphQL.API.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Department { get; set; } = default!;
    public decimal Salary { get; set; }
}