namespace Nasa.Application.Common.Interfaces;

public interface IRepository<TEntity>
{
    Task<TEntity> CreateAsync(TEntity entity);
}