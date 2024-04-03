using FluentValidation;
using Product.Application.Contracts.Repositories;

namespace Product.Application.Features.Commands.AddProduct;


public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    private readonly IProductRepository _productRepository;
    public AddProductCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        RuleFor(x => x.Code)
            .Must(x => !UniqueOperationCode(x))
            .WithMessage(x => $"{x.Code} already exists");

    }

    private bool UniqueOperationCode(string code)
        => _productRepository.IsCodeUsed(code).Result;
}

