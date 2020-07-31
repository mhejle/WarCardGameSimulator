using System;
using WarCardGameSimulator;
using Xunit;

namespace WarCardGameSimulatorTest
{
    public class DeckTest
    {
        [Fact]
        public void ADeckWithNoJokersIsPopulatedWith52Cards()
        {
            //given
            //when
            var sut = new Deck(0);

            //then
            Assert.Equal(52, sut.Count);
        }
    }
}