using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfWar
{
    class Player
    {
        public List<Card> playerCards = new List<Card>(); // Should have 26 to begin

        //Debugging purposes - ensure that the player has received a legitimate amount of cards.
        public void PrintPlayerHand()
        {
            Console.WriteLine("The player's deck contains " + playerCards.Count + " cards.");

            for (int i = 0; i < playerCards.Count; i++)
            {
                Console.WriteLine("The " + playerCards[i].CardValue + " of " + playerCards[i].CardSuit);
            }
        }

    }
}
