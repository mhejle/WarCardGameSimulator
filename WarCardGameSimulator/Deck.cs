using System;
using System.Collections.Generic;
using System.Linq;

namespace WarCardGameSimulator
{
    public class Deck : CardStack
    {
        private Deck(int numberOfJokersToInclude) : base(CreateCards(numberOfJokersToInclude))
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

        public Deck CreateDeckWithJokers(int numberOfJokersToInclude)
        {
            return new Deck(numberOfJokersToInclude);
        }

        public Deck CreateDeckNoJokers()
        {
            return new Deck(0);
        }

        public IEnumerable<Card> DrawCards(in int count)
        {
            if (count > Cards.Count)
            {
                throw new ArgumentException($"Unable to draw {count} cards - only {Cards.Count} cards left");
            }
            
            List<Card> drawnCards = new List<Card>();
            for (var i = 0; i < count; i++)
            {
                if (!TryDrawCard(out Card c))
                    throw new ArgumentException($"Unable to draw card");
                drawnCards.Add(c);
            }

            return drawnCards;
        }
    }
    
}