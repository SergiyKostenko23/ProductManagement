using FluentValidation;
using ProductManagementAPI.DTOs.Product;

/// <summary>
/// Create product validator
/// </summary>
public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    /// <summary>
    /// Create product validator constructor
    /// </summary>
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(p => p.Category)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(p => p.Price)
            .GreaterThan(0);

        RuleFor(p => p.Stock)
            .GreaterThanOrEqualTo(0);
    }
}