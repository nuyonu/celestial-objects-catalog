using Nasa.Application.Common.Interfaces;
using Nasa.Infrastructure.Persistence;
using Nasa.Shared;

namespace Nasa.Infrastructure.Repositories;

public class Repository<TEntity> : BaseRepository<TEntity>, IRepository<TEntity> where TEntity : BaseEntity
{
    public Repository(DatabaseContext context) : base(context)
    { }
    
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var addedEntity = (await this.DbSet.AddAsync(entity)).Entity;

        await this.Context.SaveChangesAsync();

        return addedEntity;
    }
}