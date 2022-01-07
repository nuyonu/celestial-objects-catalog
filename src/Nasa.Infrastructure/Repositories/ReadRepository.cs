using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nasa.Application.Common.Interfaces;
using Nasa.Infrastructure.Persistence;
using Nasa.Shared.Domain;

namespace Nasa.Infrastructure.Repositories;

public class ReadRepository<TEntity> : BaseRepository<TEntity>, IReadRepository<TEntity> where TEntity : BaseEntity
{
    public ReadRepository(DatabaseContext context) : base(context) { }

    public async Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
    {
        return SpecificationEvaluator.Default.GetQuery(DbSet.AsQueryable(), specification);
    }
}