using System.Collections.Generic;
using WarCardGameSimulator.ShufflingStrategies;

namespace WarCardGameSimulator.CardHandoutStrategies
{
    public class RandomHandoutStrategy : IHandOutStrategy
    {
        public PlayerStacks HandOutCards(Deck deck)
        {
            var playerOneCards = new List<Card>();
            var playerTwoCards = new List<Card>();
            
            ShuffleStrategyFactory shuffleStrategyFactory = new ShuffleStrategyFactory();
            deck.Shuffle(shuffleStrategyFactory.Create(ShuffleAlgorithm.Knuth));

            var cardCount = 0;
            while (deck.TryDrawCard(out Card c))
            {
                cardCount++;
                if (cardCount % 2 == 0)
                {
                    playerOneCards.Add(c);
                }
                else
                {
                    playerTwoCards.Add(c);
                }
            }
            
            
            PlayerStacks result = new PlayerStacks(new PlayerCardStack(playerTwoCards), new PlayerCardStack(playerOneCards));
            return result;
        }
        
    }
}