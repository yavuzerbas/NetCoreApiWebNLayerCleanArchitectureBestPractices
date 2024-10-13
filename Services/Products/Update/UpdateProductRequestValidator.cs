using FluentValidation;

namespace App.Services.Products.Update
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            //name validation
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .Length(3, 20).WithMessage("Product name should be between 3-20 chars");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Product price should be greater than 0");
            //product category validation
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category id should be greater than 0");
            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100).WithMessage("Product stock can be between 1-100");

        }
    }
}
