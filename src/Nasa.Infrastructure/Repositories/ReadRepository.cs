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
    
    public async Task<List<TEntity>> ListAsync(IEnumerable<ISpecification<TEntity>> specifications,
        CancellationToken cancellationToken = default)
    {
        var resultAfterSpecifications = DbSet.AsQueryable();

        resultAfterSpecifications = specifications.Aggregate(resultAfterSpecifications,
            (current, specification) => ApplySpecification(specification, current));

        return await resultAfterSpecifications.ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(id);
        }

        return entity;
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
    {
        return SpecificationEvaluator.Default.GetQuery(DbSet.AsQueryable(), specification);
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification, IQueryable<TEntity> source)
    {
        return SpecificationEvaluator.Default.GetQuery(source, specification);
    }
}