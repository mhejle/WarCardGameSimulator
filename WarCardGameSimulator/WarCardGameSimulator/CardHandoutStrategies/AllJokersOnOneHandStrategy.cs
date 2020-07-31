using WarCardGameSimulator.ShufflingStrategies;

namespace WarCardGameSimulator.CardHandoutStrategies
{
    public class AllJokersOnOneHandStrategy : IHandOutStrategy
    {
        public void HandOutCards(Deck deck, CardStack playerOne, CardStack playerTwo)
        {
            deck.GetAllCards();
            
            ShuffleStrategyFactory shuffleStrategyFactory = new ShuffleStrategyFactory();
            deck.Shuffle(shuffleStrategyFactory.Create(ShuffleAlgorithm.Knuth));

            var jokers = deck.DrawAll(Rank.Joker, Suit.None);
            playerOne.Add(jokers);
            playerTwo.Add(deck.DrawCards(jokers.Count));
            
            //TODO duplicate of RandomHandoutStrategy - could use that strategy for the rest of the deck.
            var cardCount = 0;
            while (deck.TryDrawCard(out Card c))
            {
                cardCount++;
                if (cardCount % 2 == 0)
                {
                    playerOne.Add(c);
                }
                else
                {
                    playerTwo.Add(c);
                }
            }
        }
    }
}