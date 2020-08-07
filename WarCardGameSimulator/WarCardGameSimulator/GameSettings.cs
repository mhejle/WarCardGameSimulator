using WarCardGameSimulator.DealerStrategies;
using WarCardGameSimulator.ShufflingStrategies;

namespace WarCardGameSimulator
{
    public class GameSettings
    {
        public DealerStrategyType DealerStrategyType { get; }
        public int NumberOfJokersInDeck { get; }

        public GameSettings(DealerStrategyType dealerStrategyType, int numberOfJokersInDeck)
        {
            DealerStrategyType = dealerStrategyType;
            NumberOfJokersInDeck = numberOfJokersInDeck;
        }
    }
}