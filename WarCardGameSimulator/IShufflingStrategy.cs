using System.Collections.Generic;

namespace WarCardGameSimulator
{
    public interface IShufflingStrategy
    {
        void Shuffle(IList<Card> cards);
    }
}