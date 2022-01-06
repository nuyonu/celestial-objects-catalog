using Microsoft.EntityFrameworkCore;
using Nasa.Infrastructure.Persistence;
using Nasa.Shared.Domain;

namespace Nasa.Infrastructure.Repositories;

public class BaseRepository<TEntity> where TEntity : BaseEntity
{
    protected BaseRepository(DatabaseContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    protected DatabaseContext Context { get; }

    protected DbSet<TEntity> DbSet { get; }
}