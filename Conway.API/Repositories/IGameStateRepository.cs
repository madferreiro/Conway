using Conway.API.Models;

namespace Conway.API.Repositories
{
    public interface IGameStateRepository
    {
        Task<GameState> GetByIdAsync(Guid id);
        Task<IEnumerable<GameState>> GetAllAsync();
        Task CreateAsync(GameState gameState);
        Task UpdateAsync(Guid id, GameState gameState);
        Task DeleteAsync(Guid id);
    }
}
