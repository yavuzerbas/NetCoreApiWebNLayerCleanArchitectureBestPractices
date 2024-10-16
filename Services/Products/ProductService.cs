﻿using App.Repositories;
using App.Repositories.Products;
using App.Services.ExceptionHandlers;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
        {
            var products = await productRepository.GetTopPriceProductsAsync(count);

            //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price * 1.20m, p.Stock)).ToList();
            var productsAsDto = mapper.Map<List<ProductDto>>(products);
            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }
        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {
            var products = await productRepository.GetAll().ToListAsync();
            //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
            var productsAsDto = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productsAsDto);

        }
        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            var products = await productRepository
                .GetAll()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
            var productsAsDto = mapper.Map<List<ProductDto>>(products);
            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }
        public async Task<ServiceResult<ProductDto>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return ServiceResult<ProductDto>.Fail("Product not found", HttpStatusCode.NotFound);
            }

            var productAsDto = mapper.Map<ProductDto>(product);
            return ServiceResult<ProductDto>.Success(productAsDto);
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {
            var anyProduct = await productRepository.Where(x => x.Name == request.Name).AnyAsync();
            if (anyProduct)
            {
                return ServiceResult<CreateProductResponse>.Fail("Product already exists", HttpStatusCode.BadRequest);
            }

            //intentional mock trigger to practice exception handler
            if (request.Name == "trigger_exception")
            {
                throw new CriticalException("An exception occured!");
            }

            var product = mapper.Map<Product>(request);

            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateProductResponse>
                .SuccessAsCreated(new CreateProductResponse(product!.Id), $"api/products/{product.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
        {
            var isNameExistInOtherRows = await productRepository.Where(x => x.Name == request.Name && x.Id != id).AnyAsync();
            if (isNameExistInOtherRows)
            {
                return ServiceResult.Fail("Product name already exists", HttpStatusCode.BadRequest);
            }

            var product = mapper.Map<Product>(request);
            product.Id = id;

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
        public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId);

            if (product == null)
            {
                return ServiceResult.Fail($"Product {request.ProductId} does not exist");
            }
            product.Stock = request.Stock;
            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);

        }


    }
}
