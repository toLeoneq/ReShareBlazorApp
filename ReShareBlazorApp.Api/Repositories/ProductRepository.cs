using Microsoft.EntityFrameworkCore;
using ReShareBlazorApp.Api.Date;
using ReShareBlazorApp.Api.Date.Enums;
using ReShareBlazorApp.Api.Interfaces.Repositories;

namespace ReShareBlazorApp.Api.Repositories
{

    public class ProductRepository : IProductRepository
    {
        private readonly ReshareDbContext _context;
        private readonly ILogger<ProductRepository> _logger;
        public ProductRepository(ReshareDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> AddProductAsync(Product product)
        {
            try
            {
                await _context.AddAsync(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ProductRepository] Error occured in AddProductAsync : {ex}");
                return false;
            }
        }
        public async Task<List<Product>> GetProducts(
            int number = 50,
            ProductCategory? category = null,
            string? query = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null
            )
        {
            try
            {
                var q = _context.Products.Include(p => p.user).AsQueryable();
                if(category.HasValue)
                {
                    q = q.Where(p => p.Category == category.Value);
                }
                if (!string.IsNullOrWhiteSpace(query))
                {
                    var s = query.Trim();
                    q = q.Where(p => p.Name.Contains(s) || (p.Description != null && p.Description.Contains(s)));
                }
                if (minPrice.HasValue)
                {
                    q = q.Where(p => p.Price >= minPrice.Value);
                }
                if (maxPrice.HasValue && maxPrice.Value > 0)
                {
                    q = q.Where(p => p.Price <= maxPrice.Value);
                }
                if (dateFrom.HasValue)
                {
                    q = q.Where(p => p.AddedAt >= dateFrom.Value || p.ExpiresAt >= dateFrom.Value);
                }
                if (dateTo.HasValue)
                {
                    q = q.Where(p => p.ExpiresAt <= dateTo.Value);
                }
                return await q
                    .OrderByDescending(p => p.AddedAt)
                    .Take(number)
                    .ToListAsync();
               
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ProductRepository] Error occured in GetAllProducts : {ex}");
                return new List<Product>();
            }
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            try
            {
                return await _context.Products
                    .Include(p => p.user)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ProductRepository] Error occured in GetProductByIdAsync : {ex}");
                return null;
            }
        }
    }
}
