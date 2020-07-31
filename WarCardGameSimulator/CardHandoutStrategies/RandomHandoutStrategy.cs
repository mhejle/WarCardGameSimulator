using WarCardGameSimulator.ShufflingStrategies;

namespace WarCardGameSimulator.CardHandoutStrategies
{
    public class RandomHandoutStrategy : IHandOutStrategy
    {
        public void HandOutCards(Deck deck, CardStack playerOne, CardStack playerTwo)
        {
            ShuffleStrategyFactory shuffleStrategyFactory = new ShuffleStrategyFactory();
            deck.Shuffle(shuffleStrategyFactory.Create(ShuffleAlgorithm.Knuth));

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