using ReShareBlazorApp.Api.Date;
using ReShareBlazorApp.Api.Date.Enums;

namespace ReShareBlazorApp.Api.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<bool> AddProductAsync(Product product);
        Task<List<Product>> GetProducts(
            int number = 50,
            ProductCategory? category = null,
            string? query = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null);
        Task<Product?> GetProductByIdAsync(int id);
    }
}
