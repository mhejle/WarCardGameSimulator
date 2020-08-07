using System;

namespace WarCardGameSimulator.DealerStrategies
{
    public class DealerFactory
    {
        public IDealerStrategy Create(DealerStrategyType type)
        {
            switch (type)
            {
                case DealerStrategyType.Random:
                    return new RandomDealerStrategy();
                case DealerStrategyType.AllJokersOnOneHand:
                    return new AllJokersOnOneHandDealerStrategy();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}