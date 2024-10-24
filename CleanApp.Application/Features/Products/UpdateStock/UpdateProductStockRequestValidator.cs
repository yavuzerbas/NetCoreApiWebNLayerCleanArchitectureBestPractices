﻿using FluentValidation;

namespace CleanApp.Application.Features.Products.UpdateStock
{
    public class UpdateProductStockRequestValidator : AbstractValidator<UpdateProductStockRequest>
    {
        public UpdateProductStockRequestValidator()
        {
            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100).WithMessage("Product stock can be between 1-100");
        }
    }
}
