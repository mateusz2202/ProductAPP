using AutoMapper;
using MediatR;
using Product.Application.Common.Exceptions;
using Product.Application.Contracts.Repositories;
using Product.Shared.Wrapper;

namespace Product.Application.Features.Queries.GetByCode;

public record GetProductByCodeQuery(string Code) : IRequest<Result<GetProductResponse>>;

internal class GetProductnByCodeQueryHandler(IMapper mapper, IProductRepository productRepository) : IRequestHandler<GetProductByCodeQuery, Result<GetProductResponse>>
{  
    private readonly IMapper _mapper = mapper;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Result<GetProductResponse>> Handle(GetProductByCodeQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByCode(query.Code) ?? throw new NotFoundException("Not found product");
        var mappedOperation = _mapper.Map<GetProductResponse>(product);
        return await Result<GetProductResponse>.SuccessAsync(mappedOperation);
    }
}
