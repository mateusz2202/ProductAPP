using AutoMapper;
using MediatR;
using Product.Application.Contracts.Repositories;
using Product.Application.Contracts.Services;
using Product.Shared.Wrapper;
using Product.Shared;

namespace Product.Application.Features.Queries.GetAllProduct;

public record GetAllProductsResponse(int Id, string Name, string Code);
public record GetAllProductsQuery : IRequest<Result<List<GetAllProductsResponse>>>;

public class GetAllProductsQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, ICacheService cache) : IRequestHandler<GetAllProductsQuery, Result<List<GetAllProductsResponse>>>
{
    private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICacheService _cache = cache;

    public async Task<Result<List<GetAllProductsResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var cacheData = await _cache.GetAsync<IEnumerable<Domain.Entities.Product>>(ApplicationConstants.Cache.PRODUCT_KEY);

        if (cacheData != null && cacheData.Any())
            return await Result<List<GetAllProductsResponse>>
                            .SuccessAsync(_mapper.Map<List<GetAllProductsResponse>>(cacheData));

        var result = await _unitOfWork.Repository<Domain.Entities.Product>().GetAllAsync();

        await _cache.SetAsync(ApplicationConstants.Cache.PRODUCT_KEY, result, DateTimeOffset.Now.AddMinutes(30));

        var mappedOperations = _mapper.Map<List<GetAllProductsResponse>>(result);
        return await Result<List<GetAllProductsResponse>>.SuccessAsync(mappedOperations);
    }
}

