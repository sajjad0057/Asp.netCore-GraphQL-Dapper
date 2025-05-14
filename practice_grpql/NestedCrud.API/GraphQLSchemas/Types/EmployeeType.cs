using NestedCrud.API.Models;

namespace NestedCrud.API.GraphQLSchemas.Types;

public class EmployeeType : ObjectType<Employee>
{
    protected override void Configure(IObjectTypeDescriptor<Employee> descriptor)
    {
        descriptor.Field(e => e.Id);
        descriptor.Field(e => e.Name);
        descriptor.Field(e => e.Email);
        descriptor.Field(e => e.Address);
        descriptor.Field(e => e.Departments);
    }
}

