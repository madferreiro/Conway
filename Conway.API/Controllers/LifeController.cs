using Conway.API.Models;
using Conway.API.Repositories;
using Conway.Core;
using Microsoft.AspNetCore.Mvc;

namespace Conway.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LifeController : ControllerBase
    {
        private readonly ILogger<LifeController> _logger;
        private readonly IGameStateRepository _gameStateRepository;

        public LifeController(ILogger<LifeController> logger, IGameStateRepository gameStateRepository)
        {
            _logger = logger;
            _gameStateRepository = gameStateRepository;
        }

        [HttpPost("GenerateNextState", Name = "GenerateNextState")]
        public ActionResult<IEnumerable<IEnumerable<bool>>> GenerateNextState([FromBody] BoardGenerationRequest req)
        {
            try
            {
                _logger.LogDebug("GetNextState called with the followind arguments:", new { req.Board, req.Tick } );
                var updatedBoard = GameOfLife.CalculateState(req.Board, req.Tick);
                return Ok(updatedBoard);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while generating next state.", ex);
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetGameState/{id}", Name = "GetGameState")]
        public async Task<ActionResult<GameState>> GetGameState(Guid id)
        {
            try
            {
                var gameState = await _gameStateRepository.GetByIdAsync(id);
                if (gameState == null)
                {
                    return NotFound();
                }

                return Ok(gameState);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting game state.", ex);
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("CreateGameState", Name = "CreateGameState")]
        public async Task<ActionResult<GameState>> CreateGameState([FromBody] GameState gameState)
        {
            try
            {
                await _gameStateRepository.CreateAsync(gameState);

                return CreatedAtAction("CreateGameState", null, gameState);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating game state.", ex);
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("UpdateGameState/{id}", Name = "UpdateGameState")]
        public async Task<ActionResult<GameState>> UpdateGameState(Guid id, [FromBody] GameState gameState)
        {
            try
            {
                var existingGameState = await _gameStateRepository.GetByIdAsync(id);
                if (existingGameState == null)
                {
                    return NotFound();
                }

                // Update properties of existingGameState based on the input gameState

                await _gameStateRepository.UpdateAsync(id, existingGameState);

                return Ok(existingGameState);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while updating game state.", ex);
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("DeleteGameState/{id}", Name = "DeleteGameState")]
        public async Task<ActionResult> DeleteGameState(Guid id)
        {
            try
            {
                var existingGameState = await _gameStateRepository.GetByIdAsync(id);
                if (existingGameState == null)
                {
                    return NotFound();
                }

                await _gameStateRepository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while deleting game state.", ex);
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}