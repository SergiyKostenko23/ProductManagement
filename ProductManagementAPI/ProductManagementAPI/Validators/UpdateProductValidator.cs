using FluentValidation;
using ProductManagementAPI.DTOs.Product;

/// <summary>
/// Update product validator
/// </summary>
public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
{
    /// <summary>
    /// Update product validator constructor
    /// </summary>
    public UpdateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(p => p.Description)
            .MaximumLength(500);

        RuleFor(p => p.Category)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(p => p.Price)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.Stock)
            .GreaterThanOrEqualTo(0);
    }
}