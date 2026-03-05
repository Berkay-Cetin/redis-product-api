using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductDbContext _context;
    private readonly RedisCacheService _cache;

    public ProductController(ProductDbContext context, RedisCacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    // GET ALL PRODUCTS
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        const string cacheKey = "products_all";

        var cachedProducts = await _cache.GetAsync<List<Product>>(cacheKey);

        if (cachedProducts != null)
            return Ok(cachedProducts);

        var products = await _context.Products.ToListAsync();

        await _cache.SetAsync(cacheKey, products, 5);

        return Ok(products);
    }

    // GET PRODUCT BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(Guid id)
    {
        var cacheKey = $"product_{id}";

        var cachedProduct = await _cache.GetAsync<Product>(cacheKey);

        if (cachedProduct != null)
            return Ok(cachedProduct);

        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        await _cache.SetAsync(cacheKey, product, 5);

        return Ok(product);
    }

    // CREATE PRODUCT
    [HttpPost]
    public async Task<ActionResult<Product>> Create(Product product)
    {
        product.Id = Guid.NewGuid();

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // cache invalidation
        await _cache.RemoveAsync("products_all");

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    // UPDATE PRODUCT
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Product updatedProduct)
    {
        if (id != updatedProduct.Id)
            return BadRequest();

        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;

        await _context.SaveChangesAsync();

        // cache invalidation
        await _cache.RemoveAsync($"product_{id}");
        await _cache.RemoveAsync("products_all");

        return NoContent();
    }

    // DELETE PRODUCT
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        // cache invalidation
        await _cache.RemoveAsync($"product_{id}");
        await _cache.RemoveAsync("products_all");

        return NoContent();
    }
}