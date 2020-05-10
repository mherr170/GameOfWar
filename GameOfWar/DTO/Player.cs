using System;
using System.Collections.Generic;

namespace GameOfWar.DTO
{
    public class Player
    {
        public List<Card> playerCards = new List<Card>();

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
