using Microsoft.EntityFrameworkCore;
using Product.Application.Contracts.Repositories;
using System.Linq.Expressions;

namespace Product.Presentation.Repositories;


public class ProductRepository(IRepositoryAsync<Domain.Entities.Product, int> repository) : IProductRepository
{
    private readonly IRepositoryAsync<Domain.Entities.Product, int> _repository = repository;

    public async Task<bool> IsCodeUsed(string code)
        => await _repository.Entities.AnyAsync(x => EF.Functions.Like(x.Code,code));

    public async Task<bool> Any(Expression<Func<Domain.Entities.Product, bool>> condtion)
        => await _repository.Entities.AnyAsync(condtion);

    public async Task<Domain.Entities.Product?> GetByCode(string code)
         => await _repository.Entities.FirstOrDefaultAsync(x => EF.Functions.Like(x.Code, code));


}
