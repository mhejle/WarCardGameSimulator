using System.Collections.Generic;
using System.Linq;
using WarCardGameSimulator.CardHandoutStrategies;

namespace WarCardGameSimulator
{
    /// <summary>
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
                var indexOfLastCard = Cards.Count - 1;
                card = Cards[indexOfLastCard];
                //Removing from the front will shift the other items back 1 spot,
                //so that would be an O(n) operation. Removing from the back is O(1).
                Cards.RemoveAt(indexOfLastCard);
                return true;
            }
            
            card = Card.NullObject();
            return false;
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
        
    }
}