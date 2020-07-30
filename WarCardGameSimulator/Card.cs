namespace WarCardGameSimulator
{
    public class Card
    {
        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public Suit Suit { get;}
        public Rank Rank { get;}
        
        public static Card NullObject()
        {
            return new Card(Rank.None, Suit.None);
        }

        public bool IsHigher(Card other)
        {
            //TODO handle Rank.None
            return this.Rank > other.Rank;
        }
        
    }
}