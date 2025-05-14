namespace NestedCrud.API.Models.Input;

public class UpdateEmployeeInput
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public AddressInput? Address { get; set; }
    public List<DepartmentInput>? Departments { get; set; }
}

