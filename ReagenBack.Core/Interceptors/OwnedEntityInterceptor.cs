using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ReagenBack.Core.Models;
using ReagenBack.Core.Utils;

namespace ReagenBack.Core.Interceptors;

public class OwnedEntityInterceptor(IHttpContextAccessor httpContextAccessor) 
    : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {   
        // TODO: Não dá pra injetar por ser middleware?
        var httpContext = new StructuredHttpContext(httpContextAccessor);

        if (eventData.Context == null)
            throw new Exception("DbContext não disponível.");

        if (httpContext.UserId == null)
            throw new Exception("Usuário não autenticado.");

        foreach (var entry in eventData.Context.ChangeTracker.Entries<IOwnedEntity>())
        {
            // Na criação, injeta o UserId automaticamente
            if (entry.State is EntityState.Added)
            {
                entry.Entity.UserId = httpContext.UserId.Value;
                continue;
            }

            // Na atualização e deleção, valida que pertence ao usuário atual
            if (entry.State is EntityState.Modified or EntityState.Deleted)
            {
                if (entry.Entity.UserId != httpContext.UserId)
                    throw new Exception("Entidade não pertence ao usuário atual.");
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}