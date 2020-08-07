namespace WarCardGameSimulator
{
    public interface IDealerStrategy
    {
        void DealCards(Deck deck, CardStack playerOne, CardStack playerTwo);


    }
}