
using Microsoft.EntityFrameworkCore;
using ReagenBack.Core.Contexts;
using ReagenBack.Core.Models;

namespace ReagenBack.Core.Repositories;

public class UserRepository(
    AppDbContext context
) : RepositoryBase<User>(context)
{   
    public async Task<User?> GetByProviderIdAsync(string providerId) {

        return await _context.Set<User>()
            .FirstOrDefaultAsync(u => u.AuthProviderId == providerId);
    }

    public async Task<bool> DeleteByProviderIdAsync(string providerId) {

        var user = GetByProviderIdAsync(providerId);

        if (user == null)
            return false;

        return await DeleteAsync(user.Id);
    }
}