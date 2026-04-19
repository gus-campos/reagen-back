using System.Security.Claims;

namespace ReagenBack.Core.Utils;

static class HeaderEntry
{
    public const string AuthProviderId = "authProviderId";
    public const string CurrentUserId = "currentUserId";
    public const string ProviderUserId = "providerUserId";
}

public interface IStructuredHttpContext
{
    public int? UserId { get; set; }
    public string? AuthProviderId { get; }
}

public class StructuredHttpContext(IHttpContextAccessor httpContextAccessor) : IStructuredHttpContext
{
    private HttpContext? _httpContext = httpContextAccessor.HttpContext;

    public int? UserId
    {
        get
        {
            if (_httpContext == null)
                throw new Exception("Contexto indisponível.");

            object? currentUserId;

            _httpContext.Items.TryGetValue(
                HeaderEntry.CurrentUserId, 
                out currentUserId
            );

            if (currentUserId == null)
                return null;

            if (currentUserId is not int)
                throw new Exception("CurrentUserId não é um int.");

            return (int)currentUserId;
        }

        set
        {
            if (_httpContext == null)
                throw new Exception("Contexto indisponível.");

            _httpContext.Items[HeaderEntry.CurrentUserId] = value;
        }
    }

    public string? AuthProviderId
    {
        get
        {
            if (_httpContext == null)
                throw new Exception("Contexto indisponível.");

            return _httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }   
    }
}