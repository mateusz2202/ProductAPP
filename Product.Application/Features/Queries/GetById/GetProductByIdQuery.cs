using AutoMapper;
using MediatR;
using Product.Application.Common.Exceptions;
using Product.Application.Contracts.Repositories;
using Product.Shared.Wrapper;

namespace Product.Application.Features.Queries.GetById;

public record GetProductByIdQuery(int Id) : IRequest<Result<GetProductResponse>>;

internal class GetProductnByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductnByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var operation = await _unitOfWork.Repository<Domain.Entities.Product>().GetByIdAsync(query.Id) ?? throw new NotFoundException("Not found product");
        var mappedOperation = _mapper.Map<GetProductResponse>(operation);
        return await Result<GetProductResponse>.SuccessAsync(mappedOperation);
    }
}
