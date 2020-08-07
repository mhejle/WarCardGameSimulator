using System;
using System.Collections.Generic;
using System.Linq;
using WarCardGameSimulator.DealerStrategies;

namespace WarCardGameSimulator
{
    //TODO do we just add cards to the bottom of the winners stack - or should we keep a 'graveyard' that is shuffled when the stack is emptied?
    
    public class Game
    {
        private readonly CardStack _playerOne;
        private readonly CardStack _playerTwo;
        private readonly GameResult _result;

        private readonly HashSet<string> _previousGameCombinations = new HashSet<string>();
        
        public Game(GameSettings gameSettings)
        {
            
            var deckDivisionStrategyFactory = new DealerFactory();
            var handOutStrategy = deckDivisionStrategyFactory.Create(gameSettings.DealerStrategyType);
            
            
            var deck = new Deck(gameSettings.NumberOfJokersInDeck);
            _playerOne = new CardStack();
            _playerTwo = new CardStack();
            
            handOutStrategy.DealCards(deck, _playerOne, _playerTwo);
            
            _result = new GameResult();
        }

        public GameResult Play()
        {
            while (BothPlayersHaveCards())
            {

                var currentGameCombination = GetCurrentGameCombination();
                if (CombinationHasBeenSeenBefore(currentGameCombination)) //TODO should this come after the draw or before
                {
                    _result.Outcome = $"Stale mate after {_result.NumberOfDraws} draws";
                    _result.WinnerFound = false;
                    Console.WriteLine(_result.Outcome);
                    return _result;
                }

                _previousGameCombinations.Add(currentGameCombination);//TODO should this come after the draw or before
                
                _result.NumberOfDraws++;

                if (_result.NumberOfDraws > 20000)
                {
                    _result.Outcome = "No winner found in 20000 draws";
                    _result.WinnerFound = false;
                    Console.WriteLine("No winner found in 20000 draws");
                    
                    return _result;
                }
                
                Console.WriteLine($"War: draw number: {_result.NumberOfDraws}. Cards, playerOne:{_playerOne.Count} playerTwo: {_playerTwo.Count}");
                var playerOneCard = _playerOne.DrawCard();
                var playerTwoCard =_playerTwo.DrawCard();

                CheckDrawnCards(playerOneCard, playerTwoCard);
            }

            _result.Outcome = _playerOne.IsEmpty ? "Player two won" : "Player one won";
            _result.WinnerFound = true;
            Console.WriteLine($"War: a winner was found: {_result.Outcome}");
            return _result;
        }

        private string GetCurrentGameCombination()
        {
            return $"deckOne: {_playerOne.GetStateAsString()} - deckTwo: {_playerTwo.GetStateAsString()}";
        }

        private bool CombinationHasBeenSeenBefore(string currentGameCombination)
        {
            return _previousGameCombinations.Contains(currentGameCombination);
        }

        private void CheckDrawnCards(Card playerOneCard, Card playerTwoCard)
        {
            if (playerOneCard.IsSameRank(playerTwoCard))
            {
                War(playerOneCard, playerTwoCard, new List<Card>());
            }
            else if (playerOneCard.IsHigherThan(playerTwoCard))
            {
                _playerOne.Add(playerOneCard, playerTwoCard);
            }
            else
            {
                _playerTwo.Add(playerOneCard, playerTwoCard);
            }
        }

        private void War(Card playerOneCard, Card playerTwoCard, List<Card> cardsCarriedOverFromPreviousWars)
        {
            Console.WriteLine($"War: {playerOneCard} vs {playerTwoCard}");
            _result.NumberOfWars++;
            
            //TODO introduce a war strategy as there are multiple variants of card drawing for war.
            //TODO introduce a war winning strategy (less than required does not always lead to defeat - just use the cards you have).
            if (_playerOne.Count < 4)
            {
                Console.WriteLine($"War: PlayerOne is unable to complete the battle and looses");
                _playerTwo.Add(playerOneCard, playerTwoCard);
                _playerTwo.Add(_playerOne.DrawAllCards().ToArray());
            }
            else if (_playerTwo.Count < 4)
            {
                Console.WriteLine($"War: PlayerTwo is unable to complete the battle and looses");
                _playerOne.Add(playerOneCard, playerTwoCard);
                _playerOne.Add(_playerOne.DrawAllCards().ToArray());
            }
            else
            {
                var playerOneWarBountyCards = _playerOne.DrawCards(3).ToList();
                var playerTwoWarBountyCards = _playerTwo.DrawCards(3).ToList();
                var playerOneWarCard = _playerOne.DrawCard();
                var playerTwoWarCard = _playerTwo.DrawCard();
                
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
                    _playerOne.Add(playerOneCard, playerTwoCard);
                    _playerOne.Add(playerOneWarBountyCards.ToArray());
                    _playerOne.Add(playerTwoWarBountyCards.ToArray());
                    
                    _playerOne.Add(playerOneWarCard, playerTwoWarCard);
                }
                else
                {
                    Console.WriteLine($"War: PlayerTwo won the war with {playerTwoWarBountyCards.Last()} vs {playerOneWarBountyCards.Last()}");
                    _playerTwo.Add(playerOneCard, playerTwoCard);
                    _playerTwo.Add(playerOneWarBountyCards.ToArray());
                    _playerTwo.Add(playerTwoWarBountyCards.ToArray());
                    
                    _playerTwo.Add(playerOneWarCard, playerTwoWarCard);
                }
                
            }
        }

        private bool BothPlayersHaveCards()
        {
            return !_playerOne.IsEmpty && !_playerTwo.IsEmpty;
        }
    }
}