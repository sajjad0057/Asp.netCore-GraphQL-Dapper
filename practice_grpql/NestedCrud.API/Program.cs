using NestedCrud.API.Data;
using NestedCrud.API.GraphQLSchemas.Mutations;
using NestedCrud.API.GraphQLSchemas.Queries;
using NestedCrud.API.GraphQLSchemas.Types;
using NestedCrud.API.Models.Input;
using NestedCrud.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<EmployeeQuery>()
    .AddMutationType<EmployeeMutation>()
    .AddType<EmployeeType>();


var app = builder.Build();
app.MapGraphQL();
app.Run();