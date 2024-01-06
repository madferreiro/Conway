namespace Conway.API.Models
{
    public class BoardGenerationRequest
    {
        public IEnumerable<IEnumerable<bool>>? Board { get; set; }
        public int Tick { get; set; }
    }
}
