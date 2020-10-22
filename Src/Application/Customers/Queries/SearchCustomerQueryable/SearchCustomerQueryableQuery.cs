using MediatR;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Customers.Queries.SearchCustomerQueryable
{
    public class SearchCustomerQueryableQuery : IRequest<IQueryable<Customer>>
    {
        public string SearchTerm { get; set; }
    }

    public class SearchCustomerQueryHandler : IRequestHandler<SearchCustomerQueryableQuery, IQueryable<Customer>>
    {
        private readonly INorthwindDbContext _context;

        public SearchCustomerQueryHandler(INorthwindDbContext context)
        {
            _context = context;
        }

        public Task<IQueryable<Customer>> Handle(SearchCustomerQueryableQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Customers.Where(c => c.CompanyName.Contains(request.SearchTerm)));
        }
    }
}
