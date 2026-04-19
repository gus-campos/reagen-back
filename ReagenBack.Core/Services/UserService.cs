
using ReagenBack.Core.Models;
using ReagenBack.Core.Repositories;

namespace ReagenBack.Core.Services;

public class UserService(
    UserRepository repository,
    UserMapper mapper
) 
{
    protected readonly UserRepository _repository = repository;

    public async Task<UserDto?> GetUserAsync(string authProviderId) {

        var user = await _repository.GetByProviderIdAsync(authProviderId);
        
        if (user == null) return null;
        
        return mapper.ToDto(user);
    }

    public async Task<UserDto> AddUserAsync(string authProviderId, UserDto dto) {

        // Verificar se já existe
        var user = await _repository.GetByProviderIdAsync(authProviderId);

        if (user != null)
            throw new Exception("Usuário já existente não pode ser adicionado.");

        // Id será gerado pelo banco.
        var entity = mapper.ToEntity(dto, authProviderId, 0);
        entity = await _repository.AddAsync(entity);
        return mapper.ToDto(entity);
    }

    public async Task<UserDto?> UpdateUserAsync(string authProviderId, UserDto dto) {
        
        // Buscar usuário para encontrar seu id
        var user = await _repository.GetByProviderIdAsync(authProviderId);

        if (user == null)
            return null;

        // Se não passasse o auth provider, o repositório apagaria o campo no banco
        // FIXME: Isso é necessário pq o update parcial não foi implementado
        // Exigindo entidade completa! Melhorar!
        // Se amanhã User ganhar novos campos
        // E esquecer de mapear
        // Eles serão sobrescritos no banco.
        user = mapper.ToEntity(dto, user.AuthProviderId, user.Id);
        user = await _repository.UpdateAsync(user);
        
        if (user == null) 
            throw new Exception("Inesperado que user encontrado não fosse atualizado.");

        return mapper.ToDto(user);
    }

    public async Task<bool> DeleteUserAsync(string authProviderId) {

        return await _repository.DeleteByProviderIdAsync(authProviderId);
    }
}