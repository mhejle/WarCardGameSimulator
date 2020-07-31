using System;
using System.Collections.Generic;

namespace WarCardGameSimulator.ShufflingStrategies
{
    public class KnuthShuffle : IShufflingStrategy
    {
        public void Shuffle(IList<Card> cards )
        {
            // using
            // Knuth Shuffle (see at http://rosettacode.org/wiki/Knuth_shuffle)
            // Aka Fisher–Yates_shuffle: https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
            var random = new Random();
            for (int i = 0; i < cards.Count; i++) 
            {
                int r = random.Next(i, cards.Count);
                var temp = cards[i];
                cards[i] = cards[r];
                cards[r] = temp;
            }   
        }
    }
}