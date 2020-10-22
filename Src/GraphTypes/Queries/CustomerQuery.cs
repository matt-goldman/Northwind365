using GraphQL;
using GraphQL.Types;
using GraphTypes.Types;
using MediatR;
using Northwind.Application.Customers.Queries.GetCustomer;
using Northwind.Application.Customers.Queries.GetCustomersQueryable;
using Northwind.Application.Customers.Queries.SearchCustomerQueryable;

namespace GraphTypes.Queries
{
    public class CustomerQuery : ObjectGraphType
    {
        public CustomerQuery(IMediator mediator)
        {
            Field<ListGraphType<CustomerType>>(
                "customers",
                resolve: context => { return mediator.Send(new GetCustomersQueryable()); });

            Field<CustomerType>(
                "customerById",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "customerId" }),
                resolve: context =>
                {
                    return mediator.Send(new GetCustomerQuery { CustomerId = context.GetArgument<string>("customerId") });
                });

            Field<ListGraphType<CustomerType>>(
                "customersByName",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "searchTerm" }),
                resolve: context =>
                {
                    return mediator.Send(new SearchCustomerQueryableQuery { SearchTerm = context.GetArgument<string>("searchTerm") });
                });
        }
    }
}
