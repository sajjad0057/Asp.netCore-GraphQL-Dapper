namespace GraphQL.API.DTOs;

public class CreateEmpoyeeInput
{
    public string Name { get; set; } = default!;
    public string Department { get; set; } = default!;
    public decimal Salary { get; set; }
}
