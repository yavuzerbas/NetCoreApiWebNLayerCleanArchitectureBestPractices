﻿using CleanApp.API.FIlters;
using CleanApp.Application.Features.Categories;
using CleanApp.Application.Features.Categories.Create;
using CleanApp.Application.Features.Categories.Update;
using CleanApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.API.Controllers
{
    public class CategoriesController(ICategoryService categoryService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories() =>
            CreateActionResult(await categoryService.GetAllListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id) =>
            CreateActionResult(await categoryService.GetByIdAsync(id));

        [HttpGet("products")]
        public async Task<IActionResult> GetCategoryWithProducts() =>
            CreateActionResult(await categoryService.GetCategoryWithProductsAsync());

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetCategoryWithProducts(int id) =>
            CreateActionResult(await categoryService.GetCategoryWithProductsAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request) =>
            CreateActionResult(await categoryService.CreateAsync(request));

        [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryRequest request) =>
            CreateActionResult(await categoryService.UpdateAsync(request));

        [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id) =>
            CreateActionResult(await categoryService.DeleteAsync(id));


    }
}
