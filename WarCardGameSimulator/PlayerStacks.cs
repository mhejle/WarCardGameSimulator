namespace WarCardGameSimulator
{
    public class PlayerStacks
    {
        public PlayerCardStack PlayerOneStack { get; }
        public PlayerCardStack PlayerTwoStack { get; }

        public PlayerStacks(PlayerCardStack playerOneStack, PlayerCardStack playerTwoStack)
        {
            PlayerOneStack = playerOneStack;
            PlayerTwoStack = playerTwoStack;
        }
    }
}