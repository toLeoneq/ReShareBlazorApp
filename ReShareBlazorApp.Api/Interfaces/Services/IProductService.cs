using ReShareBlazorApp.Api.Date.Enums;
using ReShareBlazorApp.Shared.DTOs;

namespace ReShareBlazorApp.Api.Interfaces.Services
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(ProductDTO product);
        Task<List<ProductDTO>> GetProductsAsync(
            int number = 50,
            ProductCategory? category = null,
            string? query = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null);
        Task<ProductDTO?> GetProductByIdAsync(int id);
    }
}
