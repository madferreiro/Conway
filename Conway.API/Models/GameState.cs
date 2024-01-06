namespace Conway.API.Models
{
    public class GameState
    {
        public Guid Id { get; set; }
        public IEnumerable<IEnumerable<bool>>? Board { get; set; }
        public int Tick { get; set; }
    }
}
