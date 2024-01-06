using Conway.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Conway.API.Repositories
{
    public class GameStateRepository : IGameStateRepository
    {
        private readonly IMongoCollection<GameState> _collection;

        public GameStateRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<GameState>("GameStates");
        }

        public async Task<GameState> GetByIdAsync(Guid id)
        {
            var filter = Builders<GameState>.Filter.Eq(g => g.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GameState>> GetAllAsync()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task CreateAsync(GameState gameState)
        {
            await _collection.InsertOneAsync(gameState);
        }

        public async Task UpdateAsync(Guid id, GameState gameState)
        {
            var filter = Builders<GameState>.Filter.Eq(g => g.Id, id);
            await _collection.ReplaceOneAsync(filter, gameState);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = Builders<GameState>.Filter.Eq(g => g.Id, id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
