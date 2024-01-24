using ProvaPub.Dtos;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class ProductService
    {
        private readonly TestDbContext _ctx;
        private readonly PagedService<Product> _pagedService;

        public ProductService(TestDbContext ctx)
        {
            this._ctx = ctx;
            _pagedService = new PagedService<Product>(ctx);
        }

        public PagedList<Product> ListProducts(int page, int pageSize = 10)
        {
            return _pagedService.ListPaged(page, pageSize, this._ctx.Products);
        }
    }
}
