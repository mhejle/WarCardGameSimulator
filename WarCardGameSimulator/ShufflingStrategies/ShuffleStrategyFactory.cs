using System;

namespace WarCardGameSimulator.ShufflingStrategies
{
    public class ShuffleStrategyFactory
    {
        public IShufflingStrategy Create(ShuffleAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case ShuffleAlgorithm.Knuth:
                    return new KnuthShuffle();
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, null);
            }
        }
    }
}