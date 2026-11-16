
using ReShareBlazorApp.Api.Date;
using ReShareBlazorApp.Api.Date.Enums;
using ReShareBlazorApp.Api.Interfaces.Repositories;
using ReShareBlazorApp.Api.Interfaces.Services;
using ReShareBlazorApp.Migrations;
using ReShareBlazorApp.Shared.DTOs;

namespace ReShareBlazorApp.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> AddProductAsync(ProductDTO product)
        {
            var productModel = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                AddedAt = product.AddedAt,
                ExpiresAt = product.ExpiresAt,
                Category = product.Category,
                UserId = product.UserId
            };
            var result = await _productRepository.AddProductAsync(productModel);
            return result;
        }
        public async Task<List<ProductDTO>> GetProductsAsync(
            int number = 50,
            ProductCategory? category = null,
            string? query = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null)
        {
            var listOfProducts = await _productRepository.GetProducts(
                number, category, query, minPrice, maxPrice, dateFrom, dateTo);
            var productsDtoList = listOfProducts.
                Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    AddedAt = p.AddedAt,
                    ExpiresAt = p.ExpiresAt,
                    Category = p.Category,
                    UserId = p.UserId,
                    UserName = p.user?.UserName
                })
                .ToList();
            return productsDtoList;
        }
        public async Task<ProductDTO?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
                return null;

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                AddedAt = product.AddedAt,
                ExpiresAt = product.ExpiresAt,
                Category = product.Category,
                UserId = product.UserId,
                UserName = product.user?.UserName
            };
        }
    }
}
