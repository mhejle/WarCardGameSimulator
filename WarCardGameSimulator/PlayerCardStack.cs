using System;
using System.Collections.Generic;

namespace WarCardGameSimulator
{
    public class PlayerCardStack : CardStack
    {

        public PlayerCardStack(IList<Card> cards) : base(cards)
        {
        }
    }
}