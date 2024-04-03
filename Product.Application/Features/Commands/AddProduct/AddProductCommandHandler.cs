using AutoMapper;
using MediatR;
using Product.Application.Contracts.Repositories;
using Product.Shared;
using Product.Shared.Wrapper;

namespace Product.Application.Features.Commands.AddProduct;

public record AddProductCommand(string Code, string Name, decimal Price) : IRequest<Result<int>>;

public class AddProductCommandHandler(
    IUnitOfWork<int> unitOfWork,
    IMapper mapper) : IRequestHandler<AddProductCommand, Result<int>>
{
    private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<int>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Domain.Entities.Product>(request);
        await _unitOfWork.Repository<Domain.Entities.Product>().AddAsync(product);
        await _unitOfWork.CommitAndRemoveCache(cancellationToken, [ApplicationConstants.Cache.PRODUCT_KEY]);       
        return await Result<int>.SuccessAsync(product.Id);
    }

}

