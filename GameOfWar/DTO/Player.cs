using System;
using System.Collections.Generic;

namespace GameOfWar.DTO
{
    public class Player
    {
        public List<Card> PlayerCards { get; private set; }

        public Player()
        {
            PlayerCards = new List<Card>();
        }

        //Debugging purposes - ensure that the player has received a legitimate amount of cards.
        public void PrintPlayerHand()
        {
            Console.WriteLine("The player's deck contains " + PlayerCards.Count + " cards.");

            for (int i = 0; i < PlayerCards.Count; i++)
            {
                Console.WriteLine("The " + PlayerCards[i].CardValue + " of " + PlayerCards[i].CardSuit);
            }
        }

    }
}
