using System;
using System.Linq;
using WarCardGameSimulator.CardHandoutStrategies;

namespace WarCardGameSimulator
{
    //TODO do we just add cards to the buttom of the winners stack - or should we keep a 'graveyard' that is shuffled when the stack is emptied?
    
    public class Game
    {
        private readonly GameSettings _gameSettings;
        private PlayerStacks _playerStacks;

        public Game(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            
            var deckDivisionStrategyFactory = new DeckDivisionStrategyFactory();
            var handOutStrategy = deckDivisionStrategyFactory.Create(_gameSettings.DeckDivisionStrategyType);
            
            var deck = new Deck(_gameSettings.NumberOfJokersInDeck);
            _playerStacks = handOutStrategy.HandOutCards(deck);
        }

        public GameResult PlayGame()
        {
            while (BothPlayersHaveCards())
            {
                var playerOneCard = _playerStacks.PlayerOneStack.DrawCard();
                var playerTwoCard =_playerStacks.PlayerTwoStack.DrawCard();

                CheckDrawnCards(playerOneCard, playerTwoCard);
            }
        }

        private void CheckDrawnCards(Card playerOneCard, Card playerTwoCard)
        {
            if (playerOneCard.IsSameRank(playerTwoCard))
            {
                War(playerOneCard, playerTwoCard);
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

        private void War(Card playerOneCard, Card playerTwoCard)
        {
            //TODO introduce a war strategy as there are multiple variants of card drawing for war.
            //TODO introduce a war winning strategy (less than required does not always lead to defeat - just use the cards you have).
            if (_playerStacks.PlayerOneStack.Count < 4)
            {
                //PlayerOne is unable to complete the battle and looses.
                _playerStacks.PlayerTwoStack.Add(playerOneCard, playerTwoCard);
                _playerStacks.PlayerTwoStack.Add(_playerStacks.PlayerOneStack.GetAllCards().ToArray());
            }
            else if (_playerStacks.PlayerTwoStack.Count < 4)
            {
                //PlayerTwo is unable to complete the battle and looses.
                _playerStacks.PlayerOneStack.Add(playerOneCard, playerTwoCard);
                _playerStacks.PlayerOneStack.Add(_playerStacks.PlayerOneStack.GetAllCards().ToArray());
            }
            else
            {
                var playerOneWarCards = _playerStacks.PlayerOneStack.DrawCards(4).ToList();
                var playerTwoWarCards = _playerStacks.PlayerOneStack.DrawCards(4).ToList();
                
                if (playerOneWarCards.Last().IsSameRank(playerTwoWarCards.Last()))
                {
                    //TODO add option for wars to continue
                    //War(playerOneCard, playerTwoCard);
                    throw new ArgumentException("continuing wars not supported");
                }
                else if (playerOneCard.IsHigherThan(playerTwoCard))
                {
                    _playerStacks.PlayerOneStack.Add(playerOneCard, playerTwoCard);
                    _playerStacks.PlayerOneStack.Add(playerOneWarCards.ToArray());
                    _playerStacks.PlayerOneStack.Add(playerTwoWarCards.ToArray());
                }
                else
                {
                    _playerStacks.PlayerTwoStack.Add(playerOneCard, playerTwoCard);
                    _playerStacks.PlayerTwoStack.Add(playerOneWarCards.ToArray());
                    _playerStacks.PlayerTwoStack.Add(playerTwoWarCards.ToArray());
                }
                
            }
        }

        private bool BothPlayersHaveCards()
        {
            return !_playerStacks.PlayerOneStack.IsEmpty && !_playerStacks.PlayerTwoStack.IsEmpty;
        }
    }
}