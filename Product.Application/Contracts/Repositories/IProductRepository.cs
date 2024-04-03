using System.Linq.Expressions;

namespace Product.Application.Contracts.Repositories;

public interface IProductRepository
{
    public Task<bool> IsCodeUsed(string code);
    public Task<bool> Any(Expression<Func<Domain.Entities.Product, bool>> condtion);
    public Task<Domain.Entities.Product?> GetByCode(string code);
}
