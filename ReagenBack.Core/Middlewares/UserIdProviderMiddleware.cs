using ReagenBack.Core.Repositories;
using ReagenBack.Core.Utils;

namespace ReagenBack.Core.Middlewares;

public class UserIdProviderMiddleware(
    UserRepository userRepository,
    IStructuredHttpContext structuredHttpContext
) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var authProviderId = structuredHttpContext.AuthProviderId;

        if (authProviderId != null)
        {
            var user = await userRepository.GetByProviderIdAsync(authProviderId);

            if (user == null)
                throw new Exception("Nenhum usuário com esse id. Deveria ser criado.");

            structuredHttpContext.UserId = user.Id;
        }
        
        await next(context);
    }
}