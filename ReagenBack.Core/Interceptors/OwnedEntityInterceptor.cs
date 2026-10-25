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

        var ownedEntries = eventData.Context.ChangeTracker.Entries<IOwnedEntity>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .ToList();

        // Só exige usuário quando há entidades com dono sendo alteradas (ex.: criar User não exige)
        if (ownedEntries.Count == 0)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        if (httpContext.UserId == null)
            throw new Exception("Usuário não autenticado.");

        foreach (var entry in ownedEntries)
        {
            // Na criação e atualização, injeta o UserId automaticamente
            // (a atualização só encontra entidades do usuário, via query filter)
            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                entry.Entity.UserId = httpContext.UserId.Value;
                continue;
            }

            // Na deleção, valida que pertence ao usuário atual
            if (entry.State is EntityState.Deleted)
            {
                if (entry.Entity.UserId != httpContext.UserId)
                    throw new Exception("Entidade não pertence ao usuário atual.");
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}