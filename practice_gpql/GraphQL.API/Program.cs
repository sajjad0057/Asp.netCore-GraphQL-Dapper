using GraphQL.API.Data;
using GraphQL.API.GraphQL.Mutations;
using GraphQL.API.GraphQL.Queries;
using GraphQL.API.GraphQL.Subscriptions;
using GraphQL.API.Repositories;
using HotChocolate.Execution.Configuration; // Ensure this namespace is included

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<EmployeeQuery>()
    .AddMutationType<EmployeeMutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions(); // Move this method to the IRequestExecutorBuilder chain

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.UseWebSockets();

app.MapGraphQL();

app.Run();
