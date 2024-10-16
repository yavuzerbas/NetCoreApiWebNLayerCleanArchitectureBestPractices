﻿using CleanApp.Application.Contracts.Persistence;
using CleanApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanApp.Persistence.Categories
{
    public class CategoryRepository(AppDbContext context) : GenericRepository<Category, int>(context), ICategoryRepository
    {

        public Task<Category?> GetCategoryWithProductsAsync(int id)
        {
            return context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Category>> GetCategoryWithProductsAsync()
        {
            return context.Categories.Include(x => x.Products).ToListAsync();
        }
    }
}
