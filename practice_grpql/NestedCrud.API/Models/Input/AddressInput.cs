namespace NestedCrud.API.Models.Input;

public class AddressInput
{
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Country { get; set; } = default!;
}
