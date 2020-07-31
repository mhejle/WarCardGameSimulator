namespace WarCardGameSimulator
{
    public class GameResult
    {
        public GameResult()
        {
            //TODO record starting state
            Winner = "undetermined";
        }

        public int NumberOfWars { get; set; }
        public int NumberOfDraws { get; set; }
        public string Winner { get; set; }
        
        public bool WinnerFound { get; set; }
    }
}