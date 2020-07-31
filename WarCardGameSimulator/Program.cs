using System;
using WarCardGameSimulator.CardHandoutStrategies;

namespace WarCardGameSimulator
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deckDivisionStrategy"></param>
        /// <param name="numberOfJokers"></param>
        static void Main(DeckDivisionStrategyType deckDivisionStrategy = DeckDivisionStrategyType.Random, int numberOfJokers = 0)
        {
            Console.WriteLine("Lets start a war");
            
            var gameSettings = new GameSettings(deckDivisionStrategy, numberOfJokers);
            var game = new Game(gameSettings);
            var gameResult = game.Play();
            
            //TODO add option to save to a file / store in sql and run multiple game for statistics
            Console.WriteLine($"----Result----");
            Console.WriteLine($"     Winner :  {gameResult.Winner}");
            Console.WriteLine($"     NumberOfDraws :  {gameResult.NumberOfDraws}");
            Console.WriteLine($"     NumberOfWars :  {gameResult.NumberOfWars}");
        }
    }
}
