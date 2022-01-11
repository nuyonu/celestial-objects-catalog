namespace Nasa.Shared.Domain;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(Guid id) : base($"Unable to found entity with id {id}.")
    { }
}