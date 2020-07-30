using System;

namespace WarCardGameSimulator.CardHandoutStrategies
{
    public class DeckDevisionStrategyFactory
    {
        public IHandOutStrategy Create(DeckDivisionStrategyType type)
        {
            switch (type)
            {
                case DeckDivisionStrategyType.Random:
                    return new RandomHandoutStrategy();
                case DeckDivisionStrategyType.AllJokersOnOneHand:
                    return new AllJokersOnOneHandStrategy();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}