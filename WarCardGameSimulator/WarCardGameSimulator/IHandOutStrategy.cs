namespace WarCardGameSimulator
{
    public interface IHandOutStrategy
    {
        void HandOutCards(Deck deck, CardStack playerOne, CardStack playerTwo);


    }
}