using GraphQL.API.Models;
using System.Threading.Tasks;

namespace GraphQL.API.GraphQL.Subscriptions;

public class Subscription
{
    [Subscribe]
    public async IAsyncEnumerable<Employee> OnEmployeeCreated([EventMessage] Employee employee)
    {
        await Task.Delay(1000); // Simulate some delay
        yield return employee;
    }

}
