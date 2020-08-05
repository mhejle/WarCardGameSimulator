using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="numberOfGamesToPlay"></param>
        static void Main(DeckDivisionStrategyType deckDivisionStrategy = DeckDivisionStrategyType.Random, int numberOfJokers = 0, int numberOfGamesToPlay = 100)
        {
            Console.WriteLine("Lets start a war");
            
            var gameSettings = new GameSettings(deckDivisionStrategy, numberOfJokers);

            var gameResults = new List<GameResult>();
            for (var i = 0; i < numberOfGamesToPlay; i++)
            {
                var game = new Game(gameSettings);
                var gameResult = game.Play();
            
                //TODO add option to save to a file / store in sql and run multiple game for statistics
                Console.WriteLine($"----Result----");
                Console.WriteLine($"     Winner :  {gameResult.Outcome}");
                Console.WriteLine($"     NumberOfDraws :  {gameResult.NumberOfDraws}");
                Console.WriteLine($"     NumberOfWars :  {gameResult.NumberOfWars}");

                gameResults.Add(gameResult);
            }

            var gamesWithWinner = gameResults.Where(_=>_.WinnerFound).ToList();
            if (gamesWithWinner.Count > 0)
            {
                Console.WriteLine($"----Results total----");
                Console.WriteLine($"     Games with winner:  {gamesWithWinner.Count}");
                Console.WriteLine($"     Avg number of draws :  {gamesWithWinner.Sum(_=>_.NumberOfDraws) / gameResults.Count}");
                Console.WriteLine($"     Avg number of wars :  {gamesWithWinner.Sum(_=>_.NumberOfWars) / gameResults.Count}");
                Console.WriteLine($"     Games with no winner:  {gameResults.Count(_=>!_.WinnerFound)}");
            }
            
        }
    }
}
