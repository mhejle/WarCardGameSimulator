using System;
using System.Collections.Generic;
using System.Linq;
using WarCardGameSimulator.CardHandoutStrategies;

namespace WarCardGameSimulator
{
    //TODO do we just add cards to the buttom of the winners stack - or should we keep a 'graveyard' that is shuffled when the stack is emptied?
    
    public class Game
    {
        private readonly PlayerStacks _playerStacks;
        private GameResult _result;

        public Game(GameSettings gameSettings)
        {
            
            var deckDivisionStrategyFactory = new DeckDivisionStrategyFactory();
            var handOutStrategy = deckDivisionStrategyFactory.Create(gameSettings.DeckDivisionStrategyType);
            
            
            var deck = new Deck(gameSettings.NumberOfJokersInDeck);
            _playerStacks = handOutStrategy.HandOutCards(deck);
            
            _result = new GameResult(_playerStacks);
        }

        public GameResult Play()
        {
            while (BothPlayersHaveCards())
            {
                _result.NumberOfDraws++;

                if (_result.NumberOfDraws > 20000)
                {
                    _result.Winner = "No winner found in 20000 draws";
                    _result.WinnerFound = false;
                    Console.WriteLine("No winner found in 20000 draws");
                    
                    return _result;
                }
                
                Console.WriteLine($"War: draw number: {_result.NumberOfDraws}. Cards, playerOne:{_playerStacks.PlayerOneStack.Count} playerTwo: {_playerStacks.PlayerTwoStack.Count}");
                var playerOneCard = _playerStacks.PlayerOneStack.DrawCard();
                var playerTwoCard =_playerStacks.PlayerTwoStack.DrawCard();

                CheckDrawnCards(playerOneCard, playerTwoCard);
            }

            _result.Winner = _playerStacks.PlayerOneStack.IsEmpty ? "Player two" : "Player one";
            _result.WinnerFound = true;
            Console.WriteLine($"War: a winner was found: {_result.Winner}");
            return _result;
        }

        private void CheckDrawnCards(Card playerOneCard, Card playerTwoCard)
        {
            if (playerOneCard.IsSameRank(playerTwoCard))
            {
                War(playerOneCard, playerTwoCard, new List<Card>());
            }
            else if (playerOneCard.IsHigherThan(playerTwoCard))
            {
                _playerStacks.PlayerOneStack.Add(playerOneCard, playerTwoCard);
            }
            else
            {
                _playerStacks.PlayerTwoStack.Add(playerOneCard, playerTwoCard);
            }
        }

        private void War(Card playerOneCard, Card playerTwoCard, List<Card> cardsCarriedOverFromPreviousWars)
        {
            Console.WriteLine($"War: {playerOneCard} vs {playerTwoCard}");
            _result.NumberOfWars++;
            
            //TODO introduce a war strategy as there are multiple variants of card drawing for war.
            //TODO introduce a war winning strategy (less than required does not always lead to defeat - just use the cards you have).
            if (_playerStacks.PlayerOneStack.Count < 4)
            {
                Console.WriteLine($"War: PlayerOne is unable to complete the battle and looses");
                _playerStacks.PlayerTwoStack.Add(playerOneCard, playerTwoCard);
                _playerStacks.PlayerTwoStack.Add(_playerStacks.PlayerOneStack.DrawAllCards().ToArray());
            }
            else if (_playerStacks.PlayerTwoStack.Count < 4)
            {
                Console.WriteLine($"War: PlayerTwo is unable to complete the battle and looses");
                _playerStacks.PlayerOneStack.Add(playerOneCard, playerTwoCard);
                _playerStacks.PlayerOneStack.Add(_playerStacks.PlayerOneStack.DrawAllCards().ToArray());
            }
            else
            {
                var playerOneWarBountyCards = _playerStacks.PlayerOneStack.DrawCards(3).ToList();
                var playerTwoWarBountyCards = _playerStacks.PlayerTwoStack.DrawCards(3).ToList();
                var playerOneWarCard = _playerStacks.PlayerOneStack.DrawCard();
                var playerTwoWarCard = _playerStacks.PlayerTwoStack.DrawCard();
                
                if (playerOneWarCard.IsSameRank(playerTwoWarCard))
                {
                    cardsCarriedOverFromPreviousWars.Add(playerOneCard);
                    cardsCarriedOverFromPreviousWars.Add(playerTwoCard);
                    
                    cardsCarriedOverFromPreviousWars.AddRange(playerOneWarBountyCards);
                    cardsCarriedOverFromPreviousWars.AddRange(playerTwoWarBountyCards);
                    
                    cardsCarriedOverFromPreviousWars.Add(playerOneWarCard);
                    cardsCarriedOverFromPreviousWars.Add(playerTwoWarCard);
                    
                    War(playerOneCard, playerTwoCard, cardsCarriedOverFromPreviousWars);
                }
                else if (playerOneWarBountyCards.Last().IsHigherThan(playerTwoWarBountyCards.Last()))
                {
                    Console.WriteLine($"War: PlayerOne won the war with {playerOneWarBountyCards.Last()} vs {playerTwoWarBountyCards.Last()}");
                    _playerStacks.PlayerOneStack.Add(playerOneCard, playerTwoCard);
                    _playerStacks.PlayerOneStack.Add(playerOneWarBountyCards.ToArray());
                    _playerStacks.PlayerOneStack.Add(playerTwoWarBountyCards.ToArray());
                    
                    _playerStacks.PlayerOneStack.Add(playerOneWarCard, playerTwoWarCard);
                }
                else
                {
                    Console.WriteLine($"War: PlayerTwo won the war with {playerTwoWarBountyCards.Last()} vs {playerOneWarBountyCards.Last()}");
                    _playerStacks.PlayerTwoStack.Add(playerOneCard, playerTwoCard);
                    _playerStacks.PlayerTwoStack.Add(playerOneWarBountyCards.ToArray());
                    _playerStacks.PlayerTwoStack.Add(playerTwoWarBountyCards.ToArray());
                    
                    _playerStacks.PlayerTwoStack.Add(playerOneWarCard, playerTwoWarCard);
                }
                
            }
        }

        private bool BothPlayersHaveCards()
        {
            return !_playerStacks.PlayerOneStack.IsEmpty && !_playerStacks.PlayerTwoStack.IsEmpty;
        }
    }
}