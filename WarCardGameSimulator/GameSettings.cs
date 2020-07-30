using WarCardGameSimulator.CardHandoutStrategies;
using WarCardGameSimulator.ShufflingStrategies;

namespace WarCardGameSimulator
{
    public class GameSettings
    {
        public ShuffleAlgorithm ShuffleAlgorithm { get; set; }
        public DeckDivisionStrategyType DeckDivisionStrategyType { get; set; }
        public int NumberOfJokersInDeck { get; set; }
        
    }
}