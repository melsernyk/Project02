namespace Project02.Models
{
    public class GameSettings
    {
        public int DeckCount { get; set; }
        public int DeckCardCount { get; set; }

        public bool AllowTie { get; set; }
        public bool AllowWildcard { get; set; }
        public string? SuitPrecedence { get; set; }
    }
}
