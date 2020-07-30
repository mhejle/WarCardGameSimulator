using System.Collections.Generic;
using WarCardGameSimulator.ShufflingStrategies;

namespace WarCardGameSimulator.CardHandoutStrategies
{
    public class AllJokersOnOneHandStrategy : IHandOutStrategy
    {
        public PlayerStacks HandOutCards(Deck deck)
        {
            deck.GetAllCards();
            
            var playerOneCards = new List<Card>();
            var playerTwoCards = new List<Card>();
            
            ShuffleStrategyFactory shuffleStrategyFactory = new ShuffleStrategyFactory();
            deck.Shuffle(shuffleStrategyFactory.Create(ShuffleAlgorithm.Knuth));

            var jokers = deck.DrawAll(Rank.Joker, Suit.None);
            playerOneCards.AddRange(jokers);
            playerTwoCards.AddRange(deck.DrawCards(jokers.Count));

            
            //TODO duplicate of RandomHandoutStrategy - could use that strategy for the rest of the deck.
            var cardCount = 0;
            while (deck.TryDrawCard(out Card c))
            {
                if (cardCount % 2 == 0)
                {
                    playerOneCards.Add(c);
                }
                else
                {
                    playerOneCards.Add(c);
                }
            }
            
            PlayerStacks result = new PlayerStacks(new PlayerCardStack(playerTwoCards), new PlayerCardStack(playerOneCards));
            return result;
        }
    }
}