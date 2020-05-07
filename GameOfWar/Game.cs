using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfWar
{
    class Game
    {

        public Game()
        {
            Deck gameDeck = new Deck();

            Player humanPlayer = new Player();
            Player computerPlayer = new Player();
            computerPlayer.isComputerPlayer = true;

            ShuffleAndDistributeCards(gameDeck, humanPlayer, computerPlayer);
        }

        private void ShuffleAndDistributeCards(Deck gameDeck, Player humanPlayer, Player computerPlayer)
        {
            Random r = new Random();

            //Loop decrements by two because one iteration deals out two cards.
            for (int numCards = 52; numCards > 0; numCards -= 2 )
            {
                //Generate a random number from 0 to one less than the amount of cards remaining in the deck.
                //It is one less because Random.Next function will return a number from 0 up to but not including the specified maximum.
                int num = r.Next(numCards - 1);

                //Take that random number, and assign the card that associates with it to the Human Player.
                humanPlayer.playerCards.Add(gameDeck.deckOfCards[num]);

                //Console.WriteLine("The " + gameDeck.deckOfCards[num].CardValue + " of " + gameDeck.deckOfCards[num].CardSuit + " has been given to the Human Player.");

                //Remove the card you just gave to the Human Player from the gameDeck
                gameDeck.deckOfCards.RemoveAt(num);

                //Generate Random number from 0 to 2 less than the cards remaining, because one card has already been removed and given it to the Human.
                num = r.Next(numCards - 2);

                //Assign the next card to the computer player.
                computerPlayer.playerCards.Add(gameDeck.deckOfCards[num]);

                //Console.WriteLine("The " + gameDeck.deckOfCards[num].CardValue + " of " + gameDeck.deckOfCards[num].CardSuit + " has been given to the Computer Player.");

                //Remove the card given to the computer player from the deck.
                gameDeck.deckOfCards.RemoveAt(num);

            }

            humanPlayer.PrintPlayerHand();
            computerPlayer.PrintPlayerHand();

        }

    }
}
