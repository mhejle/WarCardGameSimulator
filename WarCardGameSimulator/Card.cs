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

        public bool IsHigherThan(Card other)
        {
            //TODO handle Rank.None
            return Rank > other.Rank;
        }

        public bool IsSameRank(Card other)
        {
            return Rank == other.Rank;
        }

        public override string ToString()
        {
            return $"{Rank} {Suit}";
        }
    }
}