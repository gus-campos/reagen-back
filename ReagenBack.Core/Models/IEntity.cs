namespace ReagenBack.Core.Models;

public interface IEntity
{
    int Id { get; set; }
}

public interface IOwnedEntity : IEntity
{
    int UserId { get; set; }
}