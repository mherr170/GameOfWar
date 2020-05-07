using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfWar
{ 
    class Player
    {
        public bool isComputerPlayer = false;

        private string playerName;

        public List<Card> playerCards = new List<Card>(); // Should have 26 to begin

        public Player()
        {

        }

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
