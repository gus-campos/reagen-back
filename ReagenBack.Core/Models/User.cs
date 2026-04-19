
namespace ReagenBack.Core.Models;

public class User : IEntity
{
    public int Id { get; set; }
    public string AuthProviderId { get; set; } = default!;

    public string? Name { get; set; }
    public string? Email { get; set; }
}

public class UserDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
}

public class UserMapper {

    public User ToEntity(UserDto dto, string authProviderId, int id)
    {
        return new ()
        {   
            Id = id,
            AuthProviderId = authProviderId,
            Name = dto.Name,
            Email = dto.Email
        };
    }

    public UserDto ToDto(User user)
    {
        return new ()
        {
            Name = user.Name,
            Email = user.Email
        };
    }
}