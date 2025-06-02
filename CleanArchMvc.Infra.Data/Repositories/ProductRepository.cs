using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext _producContext;
        public ProductRepository(ApplicationDbContext context)
        {
            _producContext = context;
        }
        public async Task<Product> CreateAsync(Product Product)
        {
            _producContext.Add(Product);
            await _producContext.SaveChangesAsync();
            return Product;
        }

        public async Task<Product> GetByIdAsync(int? Id)
        {
            var result = await _producContext.Products.FindAsync(Id);
            return result ?? throw new Exception("Nenhum produto encontrado!");
        }

        public async Task<Product> GetProductCategoryAsync(int? Id)
        {
            var result = await _producContext.Products.Include(c => c.Category).SingleOrDefaultAsync(p => p.Id == Id);
            return result ?? throw new Exception("Nenhum produto encontrado!");
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _producContext.Products.ToListAsync();
        }

        public async Task<Product> RemoveAsync(Product product)
        {
            _producContext.Remove(product);
            await _producContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product Product)
        {
            _producContext.Update(Product);
            await _producContext.SaveChangesAsync();
            return Product;

        }
    }
}
