using Product.Domain.Contracts;

namespace Product.Domain.Entities;

public class Product : AuditableEntity<int>
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

}
