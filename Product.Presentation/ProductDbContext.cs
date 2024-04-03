using Microsoft.EntityFrameworkCore;
using Product.Application.Contracts.Services;
using Product.Domain.Contracts;

namespace Product.Presentation;

public partial class ProductDbContext(DbContextOptions<ProductDbContext> options, ICurrentUserService currentUserService) : DbContext(options)
{
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public virtual DbSet<Domain.Entities.Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Product>(x =>
        {
            x.ToTable("Product", "dbo");

            x.HasKey("Id");

            x.Property(b => b.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            SqlServerPropertyBuilderExtensions.UseIdentityColumn(x.Property<int>("Id"));

            x.Property(b => b.Code)
                .HasMaxLength(500)
                .IsRequired();

            x.Property(b => b.Name)
                .HasMaxLength(800)
                .IsRequired();
        });
        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    entry.Entity.CreatedBy = _currentUserService?.UserId ?? string.Empty;
                    entry.Entity.ModifiedOn = DateTime.UtcNow;
                    entry.Entity.ModifiedBy = _currentUserService?.UserId ?? string.Empty;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedOn = DateTime.UtcNow;
                    entry.Entity.ModifiedBy = _currentUserService?.UserId ?? string.Empty;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

}
