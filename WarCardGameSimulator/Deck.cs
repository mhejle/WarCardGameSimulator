using System;
using System.Collections.Generic;
using System.Linq;

namespace WarCardGameSimulator
{
    public class Deck : CardStack
    {
        public Deck(int numberOfJokersToInclude) : base(CreateCards(numberOfJokersToInclude))
        {
        }

        private static IList<Card> CreateCards(in int numberOfJokersToInclude)
        {
            IList<Card> cards = new List<Card>();


            foreach (var suit in EnumUtil.Values<Suit>())
            {
                foreach (var rank in EnumUtil.Values<Rank>().Except(new []{Rank.None, Rank.Joker}))
                {
                    cards.Add(new Card(rank, suit));
                }
            }

            for (var i = 0; i < numberOfJokersToInclude; i++)
            {
                cards.Add(new Card(Rank.Joker, Suit.None));
            }

            return cards;
        }
    }
    
}