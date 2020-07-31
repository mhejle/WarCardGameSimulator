namespace WarCardGameSimulator
{
    public class GameResult
    {
        public GameResult(PlayerStacks playerStacks)
        {
            //TODO record starting state
            Winner = "undetermined";
        }

        public int NumberOfWars { get; set; }
        public int NumberOfDraws { get; set; }
        public string Winner { get; set; }
    }
}