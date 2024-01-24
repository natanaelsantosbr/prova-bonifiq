using Microsoft.EntityFrameworkCore;
using ProvaPub.Dtos;

namespace ProvaPub.Services
{
    public class PagedService<T> where T : class
    {
        private readonly DbContext _ctx;

        public PagedService(DbContext ctx)
        {
            _ctx = ctx;
        }

        public PagedList<T> ListPaged(int page, int pageSize, DbSet<T> entitySet)
        {
            var totalCount = entitySet.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = entitySet
                           .Skip((page - 1) * pageSize)
                           .Take(pageSize)
                           .ToList();

            return new PagedList<T>
            {
                HasNext = page < totalPages,
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
