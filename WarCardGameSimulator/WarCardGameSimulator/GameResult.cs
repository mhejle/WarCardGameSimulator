namespace WarCardGameSimulator
{
    public class GameResult
    {
        public GameResult()
        {
            //TODO record starting state
            Outcome = "undetermined";
        }

        public int NumberOfWars { get; set; }
        public int NumberOfDraws { get; set; }
        public string Outcome { get; set; }
        
        public bool WinnerFound { get; set; }
    }
}