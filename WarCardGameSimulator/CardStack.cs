using System;
using System.Collections.Generic;
using System.Linq;

namespace WarCardGameSimulator
{
    /// <summary>
    /// This card stack is drawn from the bottom for performance reasons - see DrawCard
    /// 
    /// 
    /// Note:
    /// Inspired by: https://rosettacode.org/wiki/Playing_cards#C.23
    /// </summary>
    public abstract class CardStack 
    {
        protected List<Card> Cards { get; } //TODO consider private?

        protected CardStack(IList<Card> cards)
        {
            Cards = cards.ToList();
        }
        
        public void Shuffle(IShufflingStrategy shufflingStrategy)
        {
            shufflingStrategy.Shuffle(Cards);   
            
        }

        public bool TryDrawCard(out Card card)
        {
            if (Cards.Any())
            {
                card = DrawCard();
                return true;
            }
            
            card = Card.NullObject();
            return false;
        }

        public Card DrawCard()
        {
            var indexOfLastCard = Cards.Count - 1;
            var card = Cards[indexOfLastCard];
            //Removing from the front will shift the other items back 1 spot,
            //so that would be an O(n) operation. Removing from the back is O(1).
            Cards.RemoveAt(indexOfLastCard);
            return card;
        }


        public List<Card> DrawAll(Rank rank, Suit suit)
        {
            var matches = Cards.Where(_ => _.Rank == rank && _.Suit == suit).ToList();
            foreach (var match in matches)
                Cards.Remove(match); //TODO can this be made more optimal - find/remove in a single operation?

            return matches;
        }

        public IReadOnlyList<Card> GetAllCards()
        {
            return Cards.ToList().AsReadOnly();
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

        public void Add(params Card[] cards)
        {
            //Adding to a list using insert() is O(n*m)
            //https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.insertrange?redirectedfrom=MSDN&view=netcore-3.1#System_Collections_Generic_List_1_InsertRange_System_Int32_System_Collections_Generic_IEnumerable__0__
            
            Cards.InsertRange(0, cards);
        }
        
        public IReadOnlyList<Card> DrawAllCards()
        {
            var allCards = GetAllCards();
            Cards.Clear();

            return allCards;
        }
        
        public bool IsEmpty => Cards.Count == 0;
        public int Count => Cards.Count;
    }
}