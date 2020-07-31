using WarCardGameSimulator.CardHandoutStrategies;
using WarCardGameSimulator.ShufflingStrategies;

namespace WarCardGameSimulator
{
    public class GameSettings
    {
        public DeckDivisionStrategyType DeckDivisionStrategyType { get; }
        public int NumberOfJokersInDeck { get; }

        public GameSettings(DeckDivisionStrategyType deckDivisionStrategyType, int numberOfJokersInDeck)
        {
            DeckDivisionStrategyType = deckDivisionStrategyType;
            NumberOfJokersInDeck = numberOfJokersInDeck;
        }
    }
}