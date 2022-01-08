﻿using Ardalis.Specification;
using Nasa.Shared.Domain;

namespace Nasa.Application.Common.Interfaces;

public interface IReadRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    Task<List<TEntity>> ListAsync(IEnumerable<ISpecification<TEntity>> specifications,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default);

    Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken = default);
}