namespace Nasa.Shared.Domain;

public interface IAuditedEntity
{
    public DateTime CreatedOn { get; set; }
}